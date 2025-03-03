using Library.Core.Dtos.BookDtos;
using Library.Core.Services;
using Library.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

    public class BooksController : CustomBaseController
    {
        private readonly IBookService _bookService;
        private readonly ITokenService _tokenService;

        public BooksController(IBookService bookService, ITokenService tokenService)
        {
            _bookService = bookService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooksAsync();
            return CustomActionResult(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            return CustomActionResult(result);
        }

        [HttpGet("GetAvailableBooks")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var result = await _bookService.GetAvaliableBooksAsync();
            return CustomActionResult(result);
        }

        [HttpGet("GetBookRentalHistory/{bookId:int}")]
        public async Task<IActionResult> GetBookRentalHistory(int bookId)
        {
            var result = await _bookService.GetBookRentalHistoryAsync(bookId);
            return CustomActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto request)
        {
            var result = await _bookService.AddBookAsync(request);
            return CustomActionResult(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto request)
        {
            var result = await _bookService.UpdateBookAsync(id, request);
            return CustomActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            return CustomActionResult(result);
        }

        [HttpPost("RentBook")]
        public async Task<IActionResult> RentBook(int bookId, string userId)
        {
            var result = await _bookService.RentBookAsync(bookId, userId);
            return CustomActionResult(result);
        }
        
    }
