using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Dtos;

namespace Library.Mvc.Services.BookServices
{
    public interface IBookService
    {
        Task<ApiResponse<List<BookDto>>> GetAvailableBooks();
    }
}
