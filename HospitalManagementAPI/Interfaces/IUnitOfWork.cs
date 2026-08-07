using System;
using System.Threading.Tasks;

namespace HospitalManagementAPI.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPatientRepository Patients { get; }
    IDoctorRepository Doctors { get; }
    IUserRepository Users { get; }
    Task<int> CompleteAsync();
}
