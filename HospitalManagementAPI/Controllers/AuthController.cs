using HospitalManagementAPI.Configurations;
using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;


namespace HospitalManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IOptions<JwtSettings> jwtOptions) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        var response = await authService.RegisterAsync(requestDto);
        return StatusCode(response.statusCode,response);
    }



    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var loginResult = await authService.LoginAsync(loginRequestDto);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Ensure cookie is only transmitted over HTTPS
            SameSite = SameSiteMode.Strict, // Mitigate CSRF risk
            Expires = DateTimeOffset.UtcNow.AddMinutes(
                jwtOptions.Value.ExpiryMinutes)
        };
        Response.Cookies.Append(
            "access_token",
            loginResult.Token,
            cookieOptions);

        var response = new LoginResponseDto
        {
            Email = loginResult.Email,
            Role = loginResult.Role
        };

        return Ok(new ApiResponse<LoginResponseDto>(
        true,
        StatusCodes.Status200OK,
        "Login Successful",
        response
        ));
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");

        return Ok(
            new ApiResponse<string>(
                true,
                StatusCodes.Status200OK,
                "Logout successful.",
                string.Empty
            )
        );
    }

    [Authorize]
    [HttpGet("current-user")]
    public IActionResult CurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var role = User.FindFirstValue(ClaimTypes.Role);
        var response = new CurrentUserDto
        {
            UserId = int.Parse(userId!),
            Email = email!,
            Role = role!
        };

        return Ok(
            new ApiResponse<CurrentUserDto>(
                true,
                StatusCodes.Status200OK,
                "Current user fetched successfully.",
                response
            )
        );
    }

}