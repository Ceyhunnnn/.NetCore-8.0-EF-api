using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(UserManager<AppUser> userManager) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO request, CancellationToken cancellationToken)
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

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDTO request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(request.id.ToString());
        if (appUser is null)
        {
            return BadRequest(new{message="User not found"});
        }
        
        IdentityResult result =await userManager.ChangePasswordAsync(appUser, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors, errorCode = 400 });
        }
        return NoContent();
    }
}