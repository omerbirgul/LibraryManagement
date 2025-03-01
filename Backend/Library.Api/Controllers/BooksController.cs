using Library.Core.Dtos.BookDtos;
using Library.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

    public class BooksController : CustomBaseController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
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

        [HttpGet("GetBookRentalHistory/{id:int}")]
        public async Task<IActionResult> GetBookRentalHistory(int id)
        {
            var result = await _bookService.GetBookRentalHistoryAsync(id);
            return CustomActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDto request)
        {
            var result = await _bookService.AddBookAsync(request);
            return CustomActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(UpdateBookDto request)
        {
            var result = await _bookService.UpdateBookAsync(request);
            return CustomActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            return CustomActionResult(result);
        }
    }
