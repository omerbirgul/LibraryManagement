using Library.Core.Dtos.BookDtos;

namespace Library.Core.Services;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookByIdAsync(int id);
    Task<List<BookDto>> GetAvaliableBooksAsync();
    Task<List<BookRentalHistoryDto>> GetBookRentalHistoryAsync(int bookId);
    
    Task AddBookAsync(CreateBookDto createBookDto);
    Task UpdateBookAsync(UpdateBookDto updateBookDto);
    Task DeleteBookAsync(int bookId);
    
    Task RentBookAsync(int bookId, string userId);
    Task ReturnBookAsync(int bookId, string userId);
}