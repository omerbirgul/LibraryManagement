using Library.Core.Dtos.ResponseDto;
using Library.Core.Dtos.TokenDtos;
using Library.Core.Dtos.UserDtos;

namespace Library.Core.Services;

public interface IAuthService
{
    Task<ResultService<TokenDto>> CreateTokenAsync(LoginDto loginDto);
    Task<ResultService<TokenDto>> CreateTokenByRefreshToken(string token);
    Task<ResultService> RevokeRefreshToken(string userId);
    Task<ResultService> ApproveUser(string userId);
    Task<ResultService> AssignToAdminRole(string userId);
    Task<ResultService> AssignToManagerRole(string userId);
}