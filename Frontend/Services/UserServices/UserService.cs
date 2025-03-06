using Library.Mvc.Dtos;
using Library.Mvc.Dtos.UserDtos;
using Library.Mvc.Services.RoleServices;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Library.Mvc.Services.UserServices;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
    {
        _clientFactory = clientFactory;
        _contextAccessor = contextAccessor;
    }

    public async Task ApproveUserAsync(string userId)
    {
        var accessToken = _contextAccessor.HttpContext?.Session.GetString("token");
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new Exception("Access token not found");
        }

        var roles = GetUserRoles.GetRolesFromToken(accessToken);
        if (!roles.Contains("admin"))
        {
            throw new Exception("Not authorized");
        }

        var client = _clientFactory.CreateClient("AuthorizeClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {
            var response = await client.PatchAsJsonAsync($"http://localhost:5097/api/Auths/ApproveUser/{userId}", new { });

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API request failed: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API request failed: {ex.Message}");
        }
    }


    public async Task<ApiResponse<UserDto>> GetUserByIdAsync(string id)
    {
        var accessToken = _contextAccessor.HttpContext?.Session.GetString("token");
        if (string.IsNullOrEmpty(accessToken))
        {
            return new ApiResponse<UserDto> { ErrorMessage = "Access token not found" };
        }

        var roles = GetUserRoles.GetRolesFromToken(accessToken);
        if (!roles.Contains("admin") && !roles.Contains("manager"))
        {
            return new ApiResponse<UserDto> { ErrorMessage = "Not authorized" };
        }

        var client = _clientFactory.CreateClient("AuthorizeClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {
            var response = await client.GetFromJsonAsync<ApiResponse<UserDto>>($"http://localhost:5097/api/Users/{id}");

            if (response == null)
            {
                return new ApiResponse<UserDto> { ErrorMessage = "API response is null" };
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            return new ApiResponse<UserDto> { ErrorMessage = $"API request failed: {ex.Message}" };
        }
    }


    public async Task<ApiResponse<List<UserDto>>> GetUserListAsync()
    {
        var accessToken = _contextAccessor.HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(accessToken))
        {
            return new ApiResponse<List<UserDto>>() { ErrorMessage = "access token not found" };
        }

        var roles = GetUserRoles.GetRolesFromToken(accessToken);
        if (roles.Contains("admin") || roles.Contains("manager"))
        {
            var client = _clientFactory.CreateClient("AuthorizeClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetFromJsonAsync<ApiResponse<List<UserDto>>>("http://localhost:5097/api/Users");
            if (response is null)
            {
                return new ApiResponse<List<UserDto>>() { ErrorMessage = "api response null" };
            }

            return response;
        }
        return new ApiResponse<List<UserDto>>() { ErrorMessage = "Not authorized" };
    }

}

