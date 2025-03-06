using Library.Mvc.Dtos;
using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Services.JwtServices;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Library.Mvc.Services.BookServices;
public class BookService : IBookService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpClient _httpClient;

    public BookService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor, HttpClient httpClient)
    {
        _httpClientFactory = httpClientFactory;
        _contextAccessor = contextAccessor;
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<List<BookDto>>> GetAvailableBooks()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client
            .GetFromJsonAsync<ApiResponse<List<BookDto>>>("http://localhost:5097/api/Books/GetAvailableBooks");

        if (response is null)
        {
            throw new Exception("api response is null");
        }

        return response;
    }

    public async Task<ApiResponse<List<BookDto>>> GetBooksByTitle(string bookTitle)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client
            .GetFromJsonAsync<ApiResponse<List<BookDto>>>("http://localhost:5097/api/Books/SearchBookByName?bookName=" + bookTitle);
        if (response is null)
        {
            throw new FileNotFoundException("api response is null");
        }

        return response;
    }

    public async Task<ApiResponse> RentBookAsync(int bookId)
    {
        var accessToken = _contextAccessor.HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(accessToken))
        {
            return new ApiResponse { ErrorMessage = "Access token not found in the session" };
        }

        var userId = JwtHelper.GetClaimValue(accessToken, ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return new ApiResponse { ErrorMessage = "User Id not found in the token" };
        }
        var rentBookRequest = new RentBookRequest
        {
            BookId = bookId,
            UserId = userId
        };

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.PostAsJsonAsync("http://localhost:5097/api/Books/RentBook", rentBookRequest);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return new ApiResponse { ErrorMessage = errorMessage };
        }

        return await response.Content.ReadFromJsonAsync<ApiResponse>();
    }

    public async Task<ApiResponse> ReturnBookAsync(int bookId)
    {
        var accessToken = _contextAccessor.HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(accessToken))
        {
            return new ApiResponse { ErrorMessage = "Access token not found in the session" };
        }

        var userId = JwtHelper.GetClaimValue(accessToken, ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return new ApiResponse { ErrorMessage = "User Id not found in the token" };
        }

        var returnBookRequest = new ReturnBookRequest
        {
            BookId = bookId,
            UserId = userId
        };

        var client = _httpClientFactory.CreateClient("AuthorizeClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.PostAsJsonAsync("http://localhost:5097/api/Books/ReturnBook", returnBookRequest);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            return new ApiResponse { ErrorMessage = errorMessage };
        }
        return new ApiResponse() { };
    }
}
