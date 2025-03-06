using Library.Mvc.Dtos;
using Library.Mvc.Dtos.TokenDtos;
using System.Net.Http.Headers;

namespace Library.Mvc.Services.JwtServices;

public class RefreshTokenHandler : DelegatingHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public RefreshTokenHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _contextAccessor = contextAccessor;
    }

    private async Task<string> RefreshAccessTokenAsync()
    {
        var refreshToken = _contextAccessor.HttpContext?.Session.GetString("RefreshToken");
        if (string.IsNullOrEmpty(refreshToken))
        {
            return null;
        }

        var client = _httpClientFactory.CreateClient();
        var refreshRequest = new CreateTokenByRefreshTokenRequest { Token = refreshToken};

        var response = await client.PostAsJsonAsync("http://localhost:5097/api/Auths/CreateTokenByRefreshToken", refreshRequest);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<TokenDto>>();
        if (result.Data is null)
        {
            return null;
        }

        _contextAccessor.HttpContext.Session.SetString("token", result.Data.AccessToken);
        _contextAccessor.HttpContext.Session.SetString("RefreshToken", result.Data.RefreshToken);
        return result.Data.AccessToken;
    }


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = _contextAccessor.HttpContext.Session.GetString("token");

        if (string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var newAccessToken = await RefreshAccessTokenAsync();
            if(!string.IsNullOrEmpty(newAccessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await base.SendAsync(request,cancellationToken);
            }
        }

        return response;
    }
}

