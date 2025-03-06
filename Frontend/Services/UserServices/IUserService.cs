using Library.Mvc.Dtos.UserDtos;
using Library.Mvc.Dtos;

namespace Library.Mvc.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserDto>>> GetUserListAsync();
        Task<ApiResponse<UserDto>> GetUserByIdAsync(string id);
    }
}
