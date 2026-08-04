using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;

public interface IPatientService
{
    Task<List<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto?>  GetByIdAsync(int id);
    Task<PatientDto?> GetByUserIdAsync(int userId);
    Task<PatientDto> CreateAsync(CreatePatientDto dto);
    
    Task<bool> UpdateAsync(int id, UpdatePatientDto dto);

    Task<bool> DeleteAsync(int id);
    
}