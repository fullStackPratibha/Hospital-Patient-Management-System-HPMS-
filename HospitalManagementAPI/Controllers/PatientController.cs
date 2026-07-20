using HospitalManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalManagementAPI.DTOs;

namespace HospitalManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient(CreatePatientDto dto)
    {
        await _patientService.CreateAsync(dto);
        return Ok("Patient created successfully");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        var patients = await _patientService.GetByIdAsync(id);
        if (patients == null)
        {
            return NotFound();
        }
        return Ok(patients);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, UpdatePatientDto dto)
    {
        bool result = await _patientService.UpdateAsync(id, dto);
        if (!result)
        {
            return NotFound();
        }
        return Ok("Patient updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        bool result = await _patientService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok("Patient deleted successfully");
    }
} 