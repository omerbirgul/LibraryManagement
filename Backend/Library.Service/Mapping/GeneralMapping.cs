using AutoMapper;
using Library.Core.Dtos.BookDtos;
using Library.Core.Dtos.BookRentalDtos;
using Library.Core.Dtos.UserDtos;
using Library.Core.Entities;

namespace Library.Service.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<AppUser, RegisterUserDto>().ReverseMap();
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.UserRentalHistory,
                opt => opt.MapFrom(src => src.Rentals));

        CreateMap<BookRental, UserBookRentalDto>()
            .ForMember(dest => dest.BookTitle,
                opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.ISBN,
                opt => opt.MapFrom(src => src.Book.ISBN));

        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<Book, UpdateBookDto>().ReverseMap();
    }
}