namespace Library.Core.Dtos.BookRentalDtos;

public record UserBookRentalDto(int BookId, string BookTitle,
    string ISBN, DateTime RentalDate, DateTime? ReturnDate);