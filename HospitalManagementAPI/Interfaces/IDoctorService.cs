using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagementAPI.DTOs;

namespace HospitalManagementAPI.Interfaces;

public interface IDoctorService
{
    Task<List<DoctorDto>> GetAllDoctorsAsync();
    Task<DoctorDto?> GetDoctorByIdAsync(int id);
    Task<DoctorProfileDto?> GetDoctorByUserIdAsync(int userId);
    Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto dto);
    Task<bool> UpdateDoctorAsync(int id, UpdateDoctorDto dto);
    Task<bool> DeleteDoctorAsync(int id);
}
