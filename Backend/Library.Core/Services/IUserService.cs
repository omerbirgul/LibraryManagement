using Library.Core.Dtos.ResponseDto;
using Library.Core.Dtos.UserDtos;

namespace Library.Core.Services;

public interface IUserService
{
    Task<ResultService> RegisterAsync(RegisterUserDto registerUserDto);
    Task<ResultService<UserDto>> GetUserByIdAsync(string userId);
    Task<ResultService<List<UserDto>>> GetAllUsersAsync();
}