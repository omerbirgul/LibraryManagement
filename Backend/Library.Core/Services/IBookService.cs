using Library.Core.Dtos.BookDtos;
using Library.Core.Dtos.ResponseDto;

namespace Library.Core.Services;

public interface IBookService
{
    Task<ResultService<List<BookDto>>> GetAllBooksAsync();
    Task<ResultService<BookDto>> GetBookByIdAsync(int id);
    Task<ResultService<List<BookDto>>> GetAvaliableBooksAsync();
    Task<ResultService<List<BookRentalHistoryDto>>> GetBookRentalHistoryAsync(int bookId);
    
    Task<ResultService> AddBookAsync(CreateBookDto createBookDto);
    Task<ResultService> UpdateBookAsync(UpdateBookDto updateBookDto);
    Task<ResultService> DeleteBookAsync(int bookId);
    
    Task<ResultService> RentBookAsync(int bookId, string userId);
    Task<ResultService> ReturnBookAsync(int bookId, string userId);
}