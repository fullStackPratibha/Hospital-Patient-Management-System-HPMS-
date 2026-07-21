using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;

public interface IUserRepository
{
    Task<bool> EmailExitsAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task SaveChangesAsync();

}