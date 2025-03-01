using System.Net;
using AutoMapper;
using Library.Core.Dtos.BookDtos;
using Library.Core.Dtos.BookRentalDtos;
using Library.Core.Dtos.ResponseDto;
using Library.Core.Dtos.UserDtos;
using Library.Core.Entities;
using Library.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ResultService> RegisterAsync(RegisterUserDto registerUserDto)
    {
        var user = new AppUser()
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.Username,
            FullName = registerUserDto.FullName
        };

        var identityResult = await _userManager.CreateAsync(user, registerUserDto.Password);
        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors.Select(x => x.Description).ToList();
            return ResultService.Fail(errors);
        }

        return ResultService.Success(HttpStatusCode.Created);
    }

    public async Task<ResultService<UserDto>> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Rentals)
            .ThenInclude(br => br.Book)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return ResultService<UserDto>.Fail("User not found");

        var userDto = new UserDto(
            user.Id,
            user.FullName,
            user.Email,
            user.IsApproved,
            user.Rentals.Select(br => new UserBookRentalDto(
                br.BookId,
                br.Book.Title,
                br.Book.ISBN,
                br.RentalDate,
                br.ReturnDate
            )).ToList()
        );


        return ResultService<UserDto>.Succcess(userDto);
    }


    public async Task<ResultService<List<UserDto>>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userListDto = _mapper.Map<List<UserDto>>(users);
        return ResultService<List<UserDto>>.Succcess(userListDto);
    }
}