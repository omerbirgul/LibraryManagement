namespace Library.Core.Dtos.BookDtos;

public record BookDto(int Id, string Title, string Author, 
    string ISBN, bool IsAvaliable);