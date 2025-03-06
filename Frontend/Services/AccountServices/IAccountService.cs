using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Dtos;

namespace Library.Mvc.Services.AccountServices
{
    public interface IAccountService
    {
        Task<ApiResponse<List<BookDto>>> GetRentedBooksByUser();
        Task RevokeRefreshTokenAsync(string userId);
    }
}
