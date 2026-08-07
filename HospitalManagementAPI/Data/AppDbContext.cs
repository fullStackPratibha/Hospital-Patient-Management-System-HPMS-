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
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // User <-> Patient (One-to-One)
        modelBuilder.Entity<User>()
        .HasOne(u => u.Patient)
        .WithOne(p => p.User)
        .HasForeignKey<Patient>(p => p.UserId);

        // User <-> Doctor (One-to-One)
        modelBuilder.Entity<User>()
        .HasOne(u => u.Doctor)
        .WithOne(d => d.User)
        .HasForeignKey<Doctor>(d => d.UserId);

        // Doctor <-> Appointment (One-to-Many)
        modelBuilder.Entity<Appointment>()
        .HasOne(a => a.Doctor)
        .WithMany(d => d.Appointments)
        .HasForeignKey(a => a.DoctorId)
        .OnDelete(DeleteBehavior.Restrict);

        // Patient <-> Appointment (One-to-Many)
        modelBuilder.Entity<Appointment>()
        .HasOne(a => a.Patient)
        .WithMany(p => p.Appointments)
        .HasForeignKey(a => a.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

        // Appointment <-> MedicalRecord (One-to-One)
        modelBuilder.Entity<MedicalRecord>()
        .HasOne(m => m.Appointment)
        .WithOne(a => a.MedicalRecord)
        .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
        .OnDelete(DeleteBehavior.Cascade);

        // MedicalRecord <-> Prescription (One-to-One)
        modelBuilder.Entity<Prescription>()
        .HasOne(p => p.MedicalRecord)
        .WithOne(m => m.Prescription)
        .HasForeignKey<Prescription>(p => p.RecordId)
        .OnDelete(DeleteBehavior.Cascade);

        // Email Unique Index
        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

        // Patient Phone Number Unique Index
        modelBuilder.Entity<Patient>()
        .HasIndex(p => p.PhoneNumber)
        .IsUnique();

        // Doctor Phone Number Unique Index
        modelBuilder.Entity<Doctor>()
        .HasIndex(d => d.PhoneNumber)
        .IsUnique();
    }
}
