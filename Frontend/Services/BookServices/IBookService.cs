using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Dtos;
using Library.Mvc.Dtos.BookRentalDtos;

namespace Library.Mvc.Services.BookServices
{
    public interface IBookService
    {
        Task<ApiResponse<List<BookDto>>> GetAllBooksAsync();
        Task<ApiResponse<List<BookDto>>> GetAvailableBooks();
        Task<ApiResponse<BookDto>> GetBookById(int bookId);
        Task<ApiResponse<List<BookDto>>> GetBooksByTitle(string bookTitle);
        Task<ApiResponse<List<BookRentalHistoryDto>>> GetBookRentalHistoryById(int bookId);
        Task<ApiResponse> RentBookAsync(int bookId);
        Task<ApiResponse> ReturnBookAsync(int bookId);
        Task CreateBook(CreateBookRequest request);
        Task UpdateBook(int bookId, UpdateBookRequest request);
        Task DeleteBookAsync(int bookId);
    }
}
