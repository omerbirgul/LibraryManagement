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
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(registerDto);
        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5097/api/Users", content);
        if (response.IsSuccessStatusCode)
        {
            TempData["RegisterSuccess"] = "User registered in successfully";
            return RedirectToAction("Login", "Account");
        }
        return View();
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
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync("http://localhost:5097/api/Auths/CreateToken", loginDto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");
            return View(loginDto);
        }

        TempData["LoginFailed"] = "Login failed";

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<TokenDto>>();
        HttpContext.Session.SetString("token", result.Data.AccessToken);
        HttpContext.Session.SetString("RefreshToken", result.Data.RefreshToken);

        return RedirectToAction("Index", "Home"); 
    }

    public async Task<IActionResult> Logout()
    {
        var token = HttpContext.Session.GetString("token");
        var userId = JwtHelper.GetClaimValue(token, ClaimTypes.NameIdentifier);
        //var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        await _accountService.RevokeRefreshTokenAsync(userId);
        HttpContext.Session.Remove("token");
        HttpContext.Session.Remove("RefreshToken");
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var response = await _accountService.GetRentedBooksByUser();
        return View(response.Data);
    }
}
