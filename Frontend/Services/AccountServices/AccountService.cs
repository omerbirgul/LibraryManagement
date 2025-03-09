using Library.Mvc.Dtos;
using Library.Mvc.Dtos.BookDtos;
using Library.Mvc.Dtos.UserDtos;
using Library.Mvc.Services.JwtServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

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

        public async Task RegisterUserAsync(UserRegisterDto registerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5097/api/Users", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("User registration failed");
            }
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

            var client = _httpClientFactory.CreateClient("AuthorizeClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetFromJsonAsync<ApiResponse<List<BookDto>>>
                ("http://localhost:5097/api/Books/GetRentedBooksByUser?userId=" + userId);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return new ApiResponse<List<BookDto>> { ErrorMessage = response.ErrorMessage };
            }

            return response;
        }


        public async Task RevokeRefreshTokenAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("http://localhost:5097/api/Auths/RevokeRefreshToken/" + userId);
            if (response is null)
            {
                throw new Exception("api response is null");
            }
        }

    }
}
