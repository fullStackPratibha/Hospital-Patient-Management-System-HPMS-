using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllDoctorsAsync();
    Task<Doctor?> GetDoctorByIdAsync(int id);
    Task<Doctor?> GetDoctorByUserIdAsync(int userId);
    Task<Doctor?> GetDoctorByPhoneAsync(string phoneNumber);
    Task AddDoctorAsync(Doctor doctor);
    Task UpdateDoctorAsync(Doctor doctor);
    Task<bool> DeleteDoctorAsync(int id);
    Task SaveDoctorChangesAsync();
}
