using System;
using HospitalManagementAPI.Enums;

namespace HospitalManagementAPI.Entities;

public class Appointment
{
    public int Id { get; set; }
    
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public MedicalRecord? MedicalRecord { get; set; }
}
