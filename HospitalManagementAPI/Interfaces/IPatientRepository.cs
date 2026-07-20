using HospitalManagementAPI.Models;

namespace HospitalManagementAPI.Interfaces;
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        Task<Patient?> GetByIdAsync(int id);
    }
