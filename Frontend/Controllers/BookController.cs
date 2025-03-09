using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Services.BookServices;
using Library.Mvc.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IHttpContextAccessor _contextAccessor;

        public BookController(IBookService bookService, IHttpContextAccessor contextAccessor)
        {
            _bookService = bookService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> SearchBooksByTitle(string bookTitle)
        {
            var response = await _bookService.GetBooksByTitle(bookTitle);
            return View(response.Data);
        }

        public async Task<IActionResult> RentBook(int id)
        {
            var response = await _bookService.RentBookAsync(id);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                TempData["RentBookError"] = response.ErrorMessage;
                return RedirectToAction("Index", "Home");
            }

            TempData["RentBookSuccess"] = "Kitap başarıyla kiralandı";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ReturnBook(int id)
        {
            var response = await _bookService.ReturnBookAsync(id);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return RedirectToAction("Profile", "Account");
            }

            TempData["ReturnBookSuccess"] = "Kitap başarıyla iade edildi";
            return RedirectToAction("Profile", "Account");
        }

        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookRequest request)
        {
            await _bookService.CreateBook(request);
            return RedirectToAction("Index", "Home");
        }

    }
}
