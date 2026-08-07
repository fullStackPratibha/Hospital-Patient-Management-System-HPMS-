namespace HospitalManagementAPI.Entities;

public class MedicalRecord
{
    public int Id { get; set; }
    
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    
    public string? Diagnosis { get; set; }
    public string? Notes { get; set; }
    
    public Prescription? Prescription { get; set; }
}
