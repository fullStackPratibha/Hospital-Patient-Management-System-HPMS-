using HospitalManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HospitalManagementAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly ILogger<DoctorController> _logger;

    public DoctorController(IDoctorService doctorService, ILogger<DoctorController> _logger)
    {
        this._doctorService = doctorService;
        this._logger = _logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> GetDoctors()
    {
        _logger.LogInformation("Fetching all doctors.");
        var doctors = await _doctorService.GetAllDoctorsAsync();

        var response = new ApiResponse<List<DoctorDto>>(
            true,
            StatusCodes.Status200OK,
            "Doctors fetched successfully.",
            doctors);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto dto)
    {
        _logger.LogInformation("Creating doctor with Email {Email}", dto.Email);
        var doctor = await _doctorService.CreateDoctorAsync(dto);
        _logger.LogInformation("Doctor created successfully.");

        var response = new ApiResponse<DoctorDto>(
            true,
            StatusCodes.Status201Created,
            "Doctor created successfully.",
            doctor);

        return CreatedAtAction(
            nameof(GetDoctorById),
            new { id = doctor.Id },
            response);
    }

    [HttpGet("profile")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim.Value);
        var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
        if (doctor == null)
        {
            return NotFound();
        }

        var response = new ApiResponse<DoctorProfileDto>(
            true,
            StatusCodes.Status200OK,
            "Doctor profile fetched successfully.",
            doctor
        );

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public async Task<IActionResult> GetDoctorById([FromRoute] int id)
    {
        _logger.LogInformation("Fetching doctor with Id {Id}", id);
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }

        var response = new ApiResponse<DoctorDto>(
            true,
            StatusCodes.Status200OK,
            "Doctor fetched successfully.",
            doctor);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> UpdateDoctor([FromRoute] int id, [FromBody] UpdateDoctorDto dto)
    {
        _logger.LogInformation("Updating doctor {Id}", id);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var ownDoctor = await _doctorService.GetDoctorByUserIdAsync(int.Parse(userIdClaim.Value));
        if (ownDoctor == null || ownDoctor.Id != id)
        {
            return Forbid();
        }

        bool result = await _doctorService.UpdateDoctorAsync(id, dto);
        if (!result)
        {
            return NotFound();
        }

        var response = new ApiResponse<object>(
            true,
            StatusCodes.Status200OK,
            "Doctor updated successfully.",
            result);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
    {
        _logger.LogInformation("Deleting doctor {Id}", id);
        bool result = await _doctorService.DeleteDoctorAsync(id);
        if (!result)
        {
            return NotFound();
        }

        var response = new ApiResponse<object>(
            true,
            StatusCodes.Status200OK,
            "Doctor deleted successfully.",
            null);

        return Ok(response);
    }
}
