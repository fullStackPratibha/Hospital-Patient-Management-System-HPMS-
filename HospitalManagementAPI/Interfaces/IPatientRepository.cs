using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByUserIdAsync(int userId);
        Task<Patient?> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task SaveChangesAsync();
        Task<Patient?> GetByPhoneAsync(string phoneNumber);
        Task UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
    }
