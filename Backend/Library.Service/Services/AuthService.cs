using Library.Core.Dtos.ResponseDto;
using Library.Core.Dtos.TokenDtos;
using Library.Core.Dtos.UserDtos;
using Library.Core.Entities;
using Library.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace Library.Service.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<ResultService<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
    
        if (user is null)
        {
            return ResultService<TokenDto>.Fail("Email or Password incorrect");
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
    
        if (!isPasswordCorrect)
        {
            return ResultService<TokenDto>.Fail("Email or Password incorrect");
        }

        var token = _tokenService.CreateToken(user);
        return ResultService<TokenDto>.Succcess(token);
    }

}