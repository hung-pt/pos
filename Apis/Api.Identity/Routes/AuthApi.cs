using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Pos.Identity.Routes;

file class LoginBody {
    //public string GrantType { get; set; }
    public string ClientId { get; set; }
    //public string ClientSecret { get; set; }
}

file class TokenBody {
    public string GrantType { get; set; }
    public string RefreshToken { get; set; }
    public string ClientId { get; set; }
    //public string ClientSecret { get; set; }
}

public static class AuthApi {
    public static RouteGroupBuilder MapAuthApi(this RouteGroupBuilder group) {
        group.MapPost("/login", async (
            [FromBody] LoginBody body,
            HttpRequest request,
            JwtUtilities jwtUti
            ) => {
                string? authHeader = request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic ")) {
                    // check authHeader
                    var encodedCred = authHeader["Basic ".Length..].Trim();
                    var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCred)).Split(':');
                    var username = creds[0];
                    var password = creds[1];

                    // check creds
                    if (!(username == "man" && password == "123"))
                        return Results.Unauthorized();

                    // check client id
                    Oauth2Utilities.VerifyClientId(body.ClientId);

                    var sub = username;
                    var rt = jwtUti.GenerateRefreshToken(sub);
                    var at = jwtUti.GenerateAccessToken(sub);

                    return Results.Ok(new {
                        accessToken = at,
                        scope = "https://api.example.com",
                        tokenType = "Bearer",
                        expiresInSeconds = 3599,
                        refreshToken = rt,
                    });
                }
                else
                    return Results.Unauthorized();
            });

        group.MapPost("/token", async (
            [FromBody] TokenBody body,
            JwtUtilities jwtUti
            ) => {
                if (body.GrantType != "refresh_token")
                    return Results.BadRequest("GrantType not supported at the moment");

                if (jwtUti.ValidateRefreshToken(body.RefreshToken)) {
                    var sub = jwtUti.GetClaimByType(body.RefreshToken, "sub")?.Value;
                    var at = jwtUti.GenerateAccessToken(sub ?? "");

                    return Results.Ok(new {
                        accessToken = at,
                        scope = "https://api.example.com",
                        expiresInSeconds = 3599,
                        tokenType = "Bearer",
                    });
                }
                else return Results.BadRequest("Invalid refresh token");
            });


        group.MapGet("/_protected_resource", async () => {
            return "";
        }).RequireAuthorization();



        return group;
    }


}
