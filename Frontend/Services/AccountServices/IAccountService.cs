using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Dtos;
using Library.Mvc.Dtos.UserDtos;

namespace Library.Mvc.Services.AccountServices
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserRegisterDto registerDto);
        Task LoginAsync(UserLoginDto loginDto);
        Task LogoutAsync();
        Task<ApiResponse<List<BookDto>>> GetRentedBooksByUser();
        Task RevokeRefreshTokenAsync(string userId);
    }
}
