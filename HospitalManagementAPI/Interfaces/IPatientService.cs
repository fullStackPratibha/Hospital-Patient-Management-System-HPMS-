using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;

public interface IPatientService
{
    Task<List<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto?>  GetPatientByIdAsync(int id);
    Task<PatientProfileDto?> GetPatientByUserIdAsync(int userId);
    Task<PatientDto> CreatePatientAsync(CreatePatientDto dto);
    
    Task<bool> UpdatePatientAsync(int id, UpdatePatientDto dto);

    Task<bool> DeletePatientAsync(int id);
    
}