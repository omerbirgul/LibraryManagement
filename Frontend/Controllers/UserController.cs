using Library.Mvc.Services.UserServices;
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

        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            return View(response.Data);
        }

        public async Task<IActionResult> ApproveUser(string userId)
        {
            await _userService.ApproveUserAsync(userId);
            return RedirectToAction("GetAllUserList");
        }

        [HttpPost]
        public async Task<IActionResult> AssignToManagerRole(string userId)
        {
            await _userService.AssignToManagerRoleAsync(userId);
            return RedirectToAction("GetAllUserList");
        }

        public async Task<IActionResult> AssignToAdminRole(string userId)
        {
            await _userService.AssignToAdminRoleAsync(userId);
            return RedirectToAction("GetAllUserList");
        }
    }
}
