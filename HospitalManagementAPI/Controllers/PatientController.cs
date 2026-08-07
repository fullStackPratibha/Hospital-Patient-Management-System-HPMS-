using HospitalManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Response;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly ILogger<PatientController> _logger;

    public PatientController(IPatientService patientService, ILogger<PatientController> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetPatients()
    {
        _logger.LogInformation("Fetching all patients.");
        var patients = await _patientService.GetAllPatientsAsync();

        var response = new ApiResponse<List<PatientDto>>(
        true,
        StatusCodes.Status200OK,
        "Patients fetched successfully.",
        patients);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePatient([FromBody]CreatePatientDto dto)
    {
        _logger.LogInformation("Creating patient with PhoneNumber {PhoneNumber}",dto.PhoneNumber);
        var patient = await _patientService.CreatePatientAsync(dto);
        _logger.LogInformation("Patient created successfully.");

        var response = new ApiResponse<PatientDto>(
        true,
        StatusCodes.Status201Created,
        "Patient created successfully.",
        patient);

        return CreatedAtAction(
            nameof(GetPatientById),
            new { id = patient.Id },
            response);
    }

    [HttpGet("profile")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userIdClaim = User.FindFirst(
            System.Security.Claims.ClaimTypes.NameIdentifier
        );

        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        int userId = int.Parse(userIdClaim.Value);

        var patient = await _patientService
            .GetPatientByUserIdAsync(userId);
        

        if (patient == null)
        {
            return NotFound();
        }

        var response = new ApiResponse<PatientProfileDto>(
            true,
            StatusCodes.Status200OK,
            "Patient profile fetched successfully.",
            patient
        );

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetPatientById([FromRoute]int id)
    {
        _logger.LogInformation($"Fetching patient with Id {id}");

        // BOLA Check: Patients can only fetch their own profile
        if (User.IsInRole("Patient"))
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var ownPatient = await _patientService.GetPatientByUserIdAsync(int.Parse(userIdClaim.Value));
            if (ownPatient == null || ownPatient.Id != id)
            {
                return Forbid();
            }
        }

        var patients = await _patientService.GetPatientByIdAsync(id);
        if (patients == null)
        {
            return NotFound();
        }
        var response = new ApiResponse<PatientDto>(
            true,
            StatusCodes.Status200OK,
            "Patients fetched successfully.",
            patients);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> UpdatePatient([FromRoute]int id, [FromBody] UpdatePatientDto dto)
    {
        _logger.LogInformation("Updating patient {Id}", id);

        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var ownPatient = await _patientService.GetPatientByUserIdAsync(int.Parse(userIdClaim.Value));
        if (ownPatient == null || ownPatient.Id != id)
        {
            return Forbid();
        }

        bool result = await _patientService.UpdatePatientAsync(id, dto);
        if (!result)
        {
            return NotFound();
        }
        var response = new ApiResponse<object>(
        true,
        StatusCodes.Status200OK,
        "Patient updated successfully.",
        result);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePatient([FromRoute]int id)
    {
        _logger.LogInformation("Deleting patient {Id}", id);
        bool result = await _patientService.DeletePatientAsync(id);
        if (!result)
        {
            return NotFound();
        }
        var response = new ApiResponse<object>(
        true,
        StatusCodes.Status200OK,
        "Patient deleted successfully.",
        null);

        return Ok(response);
    }
} 