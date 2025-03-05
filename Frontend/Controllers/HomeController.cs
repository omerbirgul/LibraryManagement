using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library.Mvc.Models;
using Library.Mvc.Services.BookServices;

namespace Library.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookService _bookService;

    public HomeController(ILogger<HomeController> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var bookResponse = await _bookService.GetAvailableBooks();
        if(bookResponse is not null && string.IsNullOrEmpty(bookResponse.ErrorMessage))
        {
            return View(bookResponse.Data);
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}