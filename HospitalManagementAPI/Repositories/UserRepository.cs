using HospitalManagementAPI.Data;
using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAPI.Repositories;

public class UserRepository(AppDbContext context):IUserRepository
{
    public async Task<bool> UserEmailExistsAsync(string email)
    {
        email = email.Trim().ToLower();
        return await context.Users.AnyAsync(x => x.Email.ToLower() == email);
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        email = email.Trim().ToLower();
        return await context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);
    }
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task SaveUserChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}