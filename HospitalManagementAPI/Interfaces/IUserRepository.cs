using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;

public interface IUserRepository
{
    Task<bool> UserEmailExistsAsync(string email);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task SaveUserChangesAsync();

}