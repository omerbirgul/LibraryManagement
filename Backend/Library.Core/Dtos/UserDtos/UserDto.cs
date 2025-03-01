using Library.Core.Dtos.BookDtos;
using Library.Core.Dtos.BookRentalDtos;

namespace Library.Core.Dtos.UserDtos;

public record UserDto(string Id, string FullName,
    string Email, bool IsApproved, List<UserBookRentalDto> UserRentalHistory);