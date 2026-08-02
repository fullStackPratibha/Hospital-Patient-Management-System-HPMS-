using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Dashboard()
    {
        return Ok("Welcome Admin");
    }
}