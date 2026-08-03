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

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.AsNoTracking().Where(p=>!p.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.AsNoTracking().Where(p=>p.Id == id && !p.IsDeleted).FirstOrDefaultAsync();
        }

    public async Task<Patient?> GetByPhoneAsync(string phoneNumber)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
    }

    public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
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