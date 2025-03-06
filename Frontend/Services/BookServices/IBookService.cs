using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Dtos;

namespace Library.Mvc.Services.BookServices
{
    public interface IBookService
    {
        Task<ApiResponse<List<BookDto>>> GetAvailableBooks();
        Task<ApiResponse<List<BookDto>>> GetBooksByTitle(string bookTitle);
        Task<ApiResponse> RentBookAsync(int bookId);
        Task<ApiResponse> ReturnBookAsync(int bookId);
    }
}
