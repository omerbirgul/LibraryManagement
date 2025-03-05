using Library.Mvc.Dtos;
using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Services.JwtServices;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Library.Mvc.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountService(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiResponse<List<BookDto>>> GetRentedBooksByUser()
        {
            var accessToken = _contextAccessor.HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(accessToken))
            {
                return new ApiResponse<List<BookDto>> { ErrorMessage = "Access token not found in the session" };
            }

            var userId = JwtHelper.GetClaimValue(accessToken, ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<List<BookDto>> { ErrorMessage = "User Id not found in the token" };
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetFromJsonAsync<ApiResponse<List<BookDto>>>
                ("http://localhost:5097/api/Books/GetRentedBooksByUser?userId=" + userId);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return new ApiResponse<List<BookDto>> { ErrorMessage = response.ErrorMessage };
            }

            return response;
        }
    }
}
