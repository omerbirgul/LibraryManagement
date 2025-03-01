namespace Library.Core.Dtos.BookDtos;

public record BookRentalHistoryDto(int Id, int BookId, string BookTitle, string ISBN,
    string RentedBy, string UserId, DateTime RentalDate, DateTime? ReturnDate);