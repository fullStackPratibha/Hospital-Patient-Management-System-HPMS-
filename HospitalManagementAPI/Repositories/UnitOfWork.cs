using HospitalManagementAPI.Data;
using HospitalManagementAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace HospitalManagementAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IPatientRepository? _patients;
    private IDoctorRepository? _doctors;
    private IUserRepository? _users;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IPatientRepository Patients => _patients ??= new PatientRepository(_context);
    public IDoctorRepository Doctors => _doctors ??= new DoctorRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
