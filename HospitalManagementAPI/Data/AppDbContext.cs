using HospitalManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
        .HasOne(u => u.Patient)
        .WithOne(p => p.User)
        .HasForeignKey<Patient>(p => p.UserId);

        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
    }
}
