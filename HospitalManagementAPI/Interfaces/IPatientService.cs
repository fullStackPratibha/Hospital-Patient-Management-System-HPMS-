using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Models;

namespace HospitalManagementAPI.Interfaces;

public interface IPatientService
{
    Task<List<PatientDto>> GetAllPatientsAsync();

    Task<PatientDto> CreateAsync(CreatePatientDto dto);
}