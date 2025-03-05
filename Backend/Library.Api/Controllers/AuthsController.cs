using Library.Core.Dtos.TokenDtos;
using Library.Core.Dtos.UserDtos;
using Library.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

    public class AuthsController : CustomBaseController
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authService.CreateTokenAsync(loginDto);
            return CustomActionResult(result);
        }

        [HttpPost("CreateTokenByRefreshToken")]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateTokenByRefreshTokenRequest request)
        {
            var result = await _authService.CreateTokenByRefreshToken(request.Token);
            return CustomActionResult(result);
        }

        [HttpPatch("ApproveUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ApproveUser([FromBody] string userId)
        {
            var result = await _authService.ApproveUser(userId);
            return CustomActionResult(result);
        }

        [HttpPost("AssignToAdminRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignToAdminRole(string userName)
        {
            var result = await _authService.AssignToAdminRole(userName);
            return CustomActionResult(result);
        }

        [HttpPost("AssignToManagerRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignToManagerRole([FromBody] string userName)
        {
            var result = await _authService.AssignToManagerRole(userName);
            return CustomActionResult(result);
        }
    }
