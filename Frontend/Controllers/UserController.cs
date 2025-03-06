﻿using Library.Mvc.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Library.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> GetAllUserList()
        {
            var response = await _userService.GetUserListAsync();
            return View(response.Data);
        }
    }
}
