using HospitalManagementAPI.Data;
using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAPI.Repositories;

public class UserRepository(AppDbContext context):IUserRepository
{
    public async Task<bool> EmailExitsAsync(string email)
    {
        email = email.Trim().ToLower();
        return await context.Users.AnyAsync(x => x.Email.ToLower() == email);
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}