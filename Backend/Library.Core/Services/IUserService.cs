using Library.Core.Dtos.UserDtos;

namespace Library.Core.Services;

public interface IUserService
{
    Task RegisterAsync(RegisterUserDto registerUserDto);
    Task<UserDto> GetUserByIdAsync(string userId);
    Task<List<UserDto>> GetAllUsersAsync();
}