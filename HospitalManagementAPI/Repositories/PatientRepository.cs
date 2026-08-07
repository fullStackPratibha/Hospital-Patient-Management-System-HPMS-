using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAPI.Repositories;
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.AsNoTracking().Where(p=>!p.IsDeleted).ToListAsync();
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public async Task SavePatientChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    public async Task<Patient?> GetPatientByUserIdAsync(int userId)
    {
        return await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(
                p => p.UserId == userId
            );
    }

    public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

    public async Task<Patient?> GetPatientByPhoneAsync(string phoneNumber)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
    }

    public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }
            patient.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }