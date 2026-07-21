using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto requestDto)
    {
        var response = await authService.RegisterAsync(requestDto);
        return StatusCode(response.StatusCode,response);
    }

}