using System.IdentityModel.Tokens.Jwt;

namespace Library.Mvc.Services.JwtServices
{
    public static class JwtHelper
    {
        public static string GetClaimValue(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == claimType);
            return claim?.Value ?? "";
        }
    }
}
