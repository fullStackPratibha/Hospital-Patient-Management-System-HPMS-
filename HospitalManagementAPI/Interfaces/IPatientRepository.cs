using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByUserIdAsync(int userId);
        Task<Patient?> GetPatientByIdAsync(int id);
        Task AddPatientAsync(Patient patient);
        Task SavePatientChangesAsync();
        Task<Patient?> GetPatientByPhoneAsync(string phoneNumber);
        Task UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
    }
