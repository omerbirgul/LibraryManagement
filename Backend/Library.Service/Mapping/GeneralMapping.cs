using AutoMapper;
using Library.Core.Dtos.BookDtos;
using Library.Core.Dtos.UserDtos;
using Library.Core.Entities;

namespace Library.Service.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<AppUser, RegisterUserDto>().ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();

        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<Book, UpdateBookDto>().ReverseMap();
    }
}