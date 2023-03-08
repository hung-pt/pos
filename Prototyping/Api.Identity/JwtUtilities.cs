using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pos.Identity {
    file enum Oauth2Token {
        Access,
        Refresh
    }

    public class JwtUtilities {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _config;

        public JwtUtilities(IConfiguration config) {
            _tokenHandler = new();
            _config = config;
        }

        public string GenerateRefreshToken(string subject) =>
            GenerateJwtToken(subject, _config["Identity:RefreshSecret"]!, TimeSpan.FromDays(Convert.ToInt32(_config["Identity:RefreshLifetime"])));

        public string GenerateAccessToken(string subject) =>
            GenerateJwtToken(subject, _config["Identity:AccessSecret"]!, TimeSpan.FromDays(Convert.ToInt32(_config["Identity:AccessLifetime"])));

        public bool ValidateRefreshToken(string token) =>
            ValidateToken(token, _config["Identity:RefreshSecret"]!);

        public bool ValidateAccessToken(string token) =>
            ValidateToken(token, _config["Identity:AccessSecret"]!);

        public IEnumerable<Claim> GetClaims(string token) {
            var sToken = (JwtSecurityToken)_tokenHandler.ReadToken(token);
            return sToken.Claims;
        }

        public Claim? GetClaimByType(string token, string type) =>
            GetClaims(token).FirstOrDefault(e => e.Type == type);

        public string ExtractToken(string authHeader) {
            return authHeader["Bearer ".Length..];
        }

        public string ExtractToken(HttpRequest request) {
            string? authHeader = request.Headers["Authorization"];
            return authHeader?["Bearer ".Length..] ?? throw new Exception("Empty auth header");
        }

        private string GenerateJwtToken(string subject, string secret, TimeSpan lifetime) {
            var cred = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                SecurityAlgorithms.HmacSha256
            );

            var token = _tokenHandler.CreateToken(
                new SecurityTokenDescriptor { // https://datatracker.ietf.org/doc/html/rfc7519#section-4.1
                    Issuer = _config["Identity:Issuer"],
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, subject),
                        new Claim(JwtRegisteredClaimNames.Name, subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    }),
                    Audience = _config["Identity:Audience"],
                    Expires = DateTime.UtcNow.Add(lifetime),
                    //NotBefore
                    IssuedAt = DateTime.UtcNow,
                    // Jti
                    SigningCredentials = cred
                });

            return _tokenHandler.WriteToken(token);
        }

        private bool ValidateToken(string token, string secret) {
            try {
                var param = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),

                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at
                    // token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
                var claimsPrincipal = _tokenHandler.ValidateToken(token, param, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;

                // Return true if the token is valid and has not expired
                return (jwtToken != null) && jwtToken.ValidTo > DateTime.UtcNow;

                return true;
            }
            catch {
                return false;
            }
        }
    }
}
