using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagementAPI.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors
            .Include(d => d.User)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Doctor?> GetDoctorByUserIdAsync(int userId)
    {
        return await _context.Doctors
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.UserId == userId);
    }

    public async Task<Doctor?> GetDoctorByPhoneAsync(string phoneNumber)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.PhoneNumber == phoneNumber);
    }

    public async Task AddDoctorAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
    }

    public async Task UpdateDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return false;
        }

        // Also delete the associated User to keep things clean, or soft-delete.
        // Let's delete the doctor record and associated user
        var user = await _context.Users.FindAsync(doctor.UserId);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
        _context.Doctors.Remove(doctor);
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveDoctorChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
