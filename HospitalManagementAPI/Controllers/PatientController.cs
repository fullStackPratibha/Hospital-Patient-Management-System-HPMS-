using HospitalManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Response;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagementAPI.Controllers;

[Authorize(Roles = "Patient")]
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
    public async Task<IActionResult> CreatePatient([FromBody]CreatePatientDto dto)
    {
        _logger.LogInformation("Creating patient with email {Email}",dto.Email);
        var patient = await _patientService.CreateAsync(dto);
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientById([FromRoute]int id)
    {
        _logger.LogInformation($"Fetching patient with Id {id}");
        var patients = await _patientService.GetByIdAsync(id);
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
    public async Task<IActionResult> UpdatePatient([FromRoute]int id, [FromBody] UpdatePatientDto dto)
    {
        _logger.LogInformation("Updating patient {Id}", id);
        bool result = await _patientService.UpdateAsync(id, dto);
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
    public async Task<IActionResult> DeletePatient([FromRoute]int id)
    {
        _logger.LogInformation("Deleting patient {Id}", id);
        bool result = await _patientService.DeleteAsync(id);
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