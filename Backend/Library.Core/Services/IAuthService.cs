using Library.Core.Dtos.ResponseDto;
using Library.Core.Dtos.TokenDtos;
using Library.Core.Dtos.UserDtos;

namespace Library.Core.Services;

public interface IAuthService
{
    Task<ResultService<TokenDto>> CreateTokenAsync(LoginDto loginDto);
    Task<ResultService> ApproveUser(string userId);
    Task<ResultService> AssignToAdminRole(string userName);
    Task<ResultService> AssignToManagerRole(string userName);
}