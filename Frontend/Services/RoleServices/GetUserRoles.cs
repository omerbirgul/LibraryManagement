using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library.Mvc.Services.RoleServices
{
    public static class GetUserRoles
    {
        public static List<string> GetRolesFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var roles = jsonToken?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList() ?? new List<string>();

            return roles;
        }
    }
}
