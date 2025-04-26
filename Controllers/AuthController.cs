using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(UserManager<AppUser> userManager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO request)
    {
        AppUser appUser = new AppUser()
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        IdentityResult result = await userManager.CreateAsync(appUser, request.Password);
        if (!result.Succeeded)
        {
            // return BadRequest(result.Errors.Select(s=>s.Code)); different return
            return BadRequest(new { errors = result.Errors, errorCode = 400 });
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserData(string id)
    {
        var appUser = await userManager.FindByIdAsync(id);
        if (appUser == null)
        {
            return BadRequest(new { error = "User not found!", errorCode = 400 });
        }

        var userResponse = new
        {
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            FullName = appUser.FullName,
            Email = appUser.Email,
        };

        return Ok(userResponse);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDTO request)
    {
        AppUser? appUser = await userManager.FindByIdAsync(request.id.ToString());
        if (appUser is null)
        {
            return BadRequest(new { message = "User not found" });
        }

        IdentityResult result =
            await userManager.ChangePasswordAsync(appUser, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors, errorCode = 400 });
        }

        if (request.CurrentPassword == request.NewPassword)
        {
            return BadRequest(new { error = "Passwords same!", errorCode = 400 });
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> ForgotPassword(string? email)
    {
        AppUser? appUser = await userManager.FindByEmailAsync(email);
        if (appUser is null)
        {
            return BadRequest(new { error = "User not found" });
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(appUser);

        return Ok(new { Token = token });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePasswordUsingToken(ChangePasswordUsingTokenDTO request)
    {
        AppUser? appUser = await userManager.FindByEmailAsync(request.Email);
        if (appUser is null)
        {
            return BadRequest(new { error = "User not found" });
        }

        IdentityResult result = await userManager.ResetPasswordAsync(appUser, request.Token, request.NewPassword);
        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors, errorCode = 400 });
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO request,CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.Users.FirstOrDefaultAsync(p=>p.Email==request.UserNameOrEmail || p.UserName == request.UserNameOrEmail,cancellationToken);
        if (appUser is null)
        {
            return BadRequest(new { error = "User not found" });
        }
        Boolean result = await userManager.CheckPasswordAsync(appUser, request.Password);
        if (!result)
        {
            return BadRequest(new { error = "Password wrong" });
        }

        return Ok(new { token = "Token!" });
    }
    
}