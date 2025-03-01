using Library.Core.Dtos.UserDtos;
using Library.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterUserDto registerUserDto)
        {
            var result = await _userService.RegisterAsync(registerUserDto);
            return CustomActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return CustomActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return CustomActionResult(result);
        }
    }
