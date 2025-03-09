using Library.Mvc.Dtos;
using Library.Mvc.Dtos.TokenDtos;
using Library.Mvc.Dtos.UserDtos;
using Library.Mvc.Services.AccountServices;
using Library.Mvc.Services.JwtServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Library.Mvc.Controllers;
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountController(IHttpClientFactory httpClientFactory, IAccountService accountService, IHttpContextAccessor contextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _accountService = accountService;
        _contextAccessor = contextAccessor;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDto registerDto)
    {
        await _accountService.RegisterUserAsync(registerDto);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }

        try
        {
            await _accountService.LoginAsync(loginDto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Email veya Parola hatalı.");
            return View(loginDto);  
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var response = await _accountService.GetRentedBooksByUser();
        return View(response.Data);
    }
}
