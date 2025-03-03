using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Library.Core.Dtos.TokenDtos;
using Library.Core.Entities;
using Library.Core.Services;
using Library.Core.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library.Service.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public TokenDto CreateToken(AppUser user)
    {
        var securityKey = SignService.GetSymmetricSecurityKey(_jwtSettings.SecretKey);

        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claimsList = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };
        
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(3),
            claims: claimsList,
            signingCredentials: credentials);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(securityToken);
        var tokenDto = new TokenDto()
        {
            Token = token
        };
        return tokenDto;
    }
    
}