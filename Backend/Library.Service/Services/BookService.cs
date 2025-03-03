using System.Net;
using AutoMapper;
using Library.Core.Dtos.BookDtos;
using Library.Core.Dtos.ResponseDto;
using Library.Core.Entities;
using Library.Core.Repositories;
using Library.Core.Services;
using Library.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Service.Services;

public class BookService : IBookService
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<BookRental> _bookRentalRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IGenericRepository<Book> bookRepository, IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<BookRental> bookRentalRepository, UserManager<AppUser> userManager)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _bookRentalRepository = bookRentalRepository;
        _userManager = userManager;
    }
    
    public async Task<ResultService<List<BookDto>>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAll().ToListAsync();
        var booksAsDto = _mapper.Map<List<BookDto>>(books);
        return ResultService<List<BookDto>>.Succcess(booksAsDto);
    }

    public async Task<ResultService<BookDto>> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
            return ResultService<BookDto>.Fail("Book not found", HttpStatusCode.NotFound);
        var bookAsDto = _mapper.Map<BookDto>(book);
        return ResultService<BookDto>.Succcess(bookAsDto);
    }

    public async Task<ResultService<List<BookDto>>> GetBooksByTitle(string title)
    {
        var books = await _bookRepository.Where(b => b.Title == title).ToListAsync();
        if (books is null || books.Count == 0)
            return ResultService<List<BookDto>>.Fail("Book not found", HttpStatusCode.NotFound);
        var booksAsDto = _mapper.Map<List<BookDto>>(books);
        return ResultService<List<BookDto>>.Succcess(booksAsDto);
    }

    public async Task<ResultService<List<BookDto>>> GetAvaliableBooksAsync()
    {
        var availableBooks = await _bookRepository
            .Where(b => b.IsAvaliable).ToListAsync();
        
        if (!availableBooks.Any())
            return ResultService<List<BookDto>>.Fail("Avaliable Books not found", HttpStatusCode.NotFound);
        
        var availableBooksAsDto = _mapper.Map<List<BookDto>>(availableBooks);
        return ResultService<List<BookDto>>.Succcess(availableBooksAsDto);
    }

    public async Task<ResultService<List<BookRentalHistoryDto>>> GetBookRentalHistoryAsync(int bookId)
    {
        var rentalHistory = await _bookRentalRepository
            .Where(br => br.BookId == bookId)
            .Include(br => br.User)
            .Include(br => br.Book)
            .ToListAsync();

        if (!rentalHistory.Any())
            return ResultService<List<BookRentalHistoryDto>>.Fail("This book has no rental history");

        var rentalHistoryDtos = rentalHistory.Select(x => new BookRentalHistoryDto
            (
                x.BookId,
                x.Book.Title,
                x.Book.ISBN,
                x.User.FullName,
                x.UserId,
                x.RentalDate,
                x.ReturnDate
            )
        ).ToList();
        return ResultService<List<BookRentalHistoryDto>>.Succcess(rentalHistoryDtos);
    }

    public async Task<ResultService> AddBookAsync(CreateBookDto createBookDto)
    {
        var isBookExist = await _bookRepository
            .Where(b => b.ISBN == createBookDto.ISBN).AnyAsync();
        if (isBookExist)
            return ResultService.Fail("This ISBN number already used");
        var book = _mapper.Map<Book>(createBookDto);
        await _bookRepository.CreateAsync(book);
        await _unitOfWork.SaveChangesAsync();
        return ResultService.Success();
    }

    public async Task<ResultService> UpdateBookAsync(int bookId, UpdateBookDto updateBookDto)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book is null)
            return ResultService.Fail("No Book found to update", HttpStatusCode.NotFound);

        bool isIsbnExist = await _bookRepository
            .Where(b => b.ISBN == updateBookDto.ISBN && book.ISBN != b.ISBN)
            .AnyAsync();
        if (isIsbnExist)
            return ResultService.Fail("This ISBN number already in use");

        book = _mapper.Map(updateBookDto, book);
        _bookRepository.Update(book);
        await _unitOfWork.SaveChangesAsync();
        return ResultService.Success(HttpStatusCode.NoContent);
    }

    public async Task<ResultService> DeleteBookAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book is null)
            return ResultService.Fail("No books found to delete", HttpStatusCode.NotFound);
        
        _bookRepository.Delete(book);
        await _unitOfWork.SaveChangesAsync();
        return ResultService.Success(HttpStatusCode.NoContent);
    }

    public async Task<ResultService> RentBookAsync(int bookId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ResultService.Fail("User not found");
        
        var book = await _bookRepository.GetByIdAsync(bookId);
        if(book is null)
            return ResultService.Fail("Book not found");
    
        if(!book.IsAvaliable)
            return ResultService.Fail("Book alread rented");

        var bookRental = new BookRental()
        {
            BookId = bookId,
            UserId = userId,
            RentalDate = DateTime.Now,
            ReturnDate = null
        };

        await _bookRentalRepository.CreateAsync(bookRental);
        
        book.IsAvaliable = false;
        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync();
        return ResultService.Success();
    }

    public Task<ResultService> ReturnBookAsync(int bookId, string userId)
    {
        throw new NotImplementedException();
    }
}