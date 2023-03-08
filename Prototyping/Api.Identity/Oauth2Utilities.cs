using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pos.Identity {
    public static class Oauth2Utilities {
        private static readonly Dictionary<string, string> clients = new() {
            {
                "x9YR0nX0RfAsW5hw00seN4Jx",
                "fSEDaK7qVD7W5fId9rqp09yPfbPingdpHTjGIaz-ukjAjTEJ"
            },
        };

        public static bool VerifyClientId(string clientId) =>
            clients.ContainsKey(clientId);
    }

}
