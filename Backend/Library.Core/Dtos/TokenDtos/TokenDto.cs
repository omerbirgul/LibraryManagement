namespace Library.Core.Dtos.TokenDtos;

public record TokenDto(
    string AccessToken,
    DateTime AccessTokenExpiration,
    string RefreshToken,
    DateTime RefreshTokenExpiration);
