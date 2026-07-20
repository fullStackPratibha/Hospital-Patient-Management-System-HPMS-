using HospitalManagementAPI.Models;

namespace HospitalManagementAPI.Interfaces;
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task<bool> ExistsAsync(string email, string phoneNumber);
        Task UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
    }
