using Library.Core.Dtos.TokenDtos;
using Library.Core.Entities;

namespace Library.Core.Services;

public interface ITokenService
{
    TokenDto CreateToken(AppUser user);
}