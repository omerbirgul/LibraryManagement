using System.Net;
using Library.Core.Dtos.ResponseDto;
using Library.Core.Dtos.TokenDtos;
using Library.Core.Dtos.UserDtos;
using Library.Core.Entities;
using Library.Core.Services;
using Library.Core.Settings;
using Library.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Library.Service.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SuperAdminSettings _superAdminSettings;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService,
        RoleManager<AppRole> roleManager, IOptions<SuperAdminSettings> superAdminSettings, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _superAdminSettings = superAdminSettings.Value;
    }

    public async Task<ResultService<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
    
        if (user is null)
        {
            return ResultService<TokenDto>.Fail("Email or Password incorrect");
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
    
        if (!isPasswordCorrect)
        {
            return ResultService<TokenDto>.Fail("Email or Password incorrect");
        }

        if (user.IsApproved is false)
        {
            return ResultService<TokenDto>.Fail("Admin has not approved the membership yet.");
        }

        var token = _tokenService.CreateToken(user);
        return ResultService<TokenDto>.Succcess(token);
    }

    public async Task<ResultService> ApproveUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ResultService.Fail("User not found");

        user.IsApproved = true;
        await _userManager.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return ResultService.Success(HttpStatusCode.NoContent);
    }

    public async Task<ResultService> AssignToAdminRole(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return ResultService.Fail("User not found");
        }

        if (user.IsApproved is false)
        {
            return ResultService.Fail("Admin has not approved the membership yet.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Contains("admin"))
        {
            return ResultService.Fail("User is already an admin");
        }

        await AssignToRoleAsync(user, "admin");
        await AssignToRoleAsync(user, "manager");
        return ResultService.Success(HttpStatusCode.NoContent);
    }

    public async Task<ResultService> AssignToManagerRole(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return ResultService.Fail("User not found");
        }
        
        if (user.IsApproved is false)
        {
            return ResultService.Fail("Admin has not approved the membership yet.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Contains("manager"))
        {
            return ResultService.Fail("User is already an manager");
        }

        await AssignToRoleAsync(user, "manager");
        return ResultService.Success(HttpStatusCode.NoContent);
    }


    private async Task AssignToRoleAsync(AppUser user, string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await _roleManager.CreateAsync(new AppRole() { Name = roleName });
        }

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task EnsureSuperAdminAsync()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
        {
            await _roleManager.CreateAsync(new AppRole() { Name = "admin" });
        }

        if (!await _roleManager.RoleExistsAsync("manager"))
        {
            await _roleManager.CreateAsync(new AppRole() { Name = "manager" });
        }

        var superAdminUser = await _userManager.FindByEmailAsync(_superAdminSettings.Email);
        if (superAdminUser is null)
        {
            superAdminUser = new AppUser()
            {
                UserName = _superAdminSettings.UserName,
                Email = _superAdminSettings.Email,
                EmailConfirmed = true,
                FullName = "Omer Faruk Birgul"
            };

            var createResult = await _userManager.CreateAsync(superAdminUser, _superAdminSettings.Password);
            if (!createResult.Succeeded)
            {
                throw new Exception("Super Admin User couldn't created" +
                                    string.Join(",", createResult.Errors.Select(e => e.Description)));
            }
        }


        if (!await _userManager.IsInRoleAsync(superAdminUser, "admin"))
        {
            await _userManager.AddToRoleAsync(superAdminUser, "admin");
            await _userManager.AddToRoleAsync(superAdminUser, "manager");
        }
    }

}