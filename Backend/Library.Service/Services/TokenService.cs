using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Library.Core.Dtos.TokenDtos;
using Library.Core.Entities;
using Library.Core.Services;
using Library.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.Service.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<AppUser> _userManager;

    public TokenService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    private async Task<IEnumerable<Claim>> GetUserClaims(AppUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaimList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        userClaimList.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        return userClaimList;
    }

    private string CreateRefreshToken()
    {
        var numberBytes = new Byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberBytes);
        return Convert.ToBase64String(numberBytes);
    }

    public TokenDto CreateToken(AppUser user)
    {
        var accessTokenExpiration = DateTime.Now.AddSeconds(_jwtSettings.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiration);
        var securityKey = SignService.GetSymmetricSecurityKey(_jwtSettings.SecretKey);

        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(3),
            claims: GetUserClaims(user).Result,
            signingCredentials: credentials);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(securityToken);
        var tokenDto = new TokenDto
        (
            token,
            accessTokenExpiration,
            CreateRefreshToken(),
            refreshTokenExpiration
        );
        return tokenDto;
    }
}