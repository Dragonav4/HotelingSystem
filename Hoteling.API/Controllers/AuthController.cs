using System.Security.Claims;
using Hoteling.API.Exceptions;
using Hoteling.Application.Interfaces.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hoteling.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login([FromQuery] string? returnUrl)
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "/"
            }, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout([FromQuery] string? returnUrl)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var redirectUri = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "http://localhost:7000";
        return Redirect(redirectUri);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("Unauthorized: Email claim is missing.");
        }

        var user = await userService.GetUserByEmail(email)
                   ?? throw new UserNotFoundException(email);
        var pictureUrl = User.FindFirst("picture")?.Value;

        return Ok(new
        {
            user.Id,
            user.Email,
            user.UserName,
            user.Role,
            picture = pictureUrl
        });
    }
}
