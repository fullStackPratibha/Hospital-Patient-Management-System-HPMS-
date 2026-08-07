using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Entities;

public class Doctor
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; } = 0;
    public string PhoneNumber { get; set; } = string.Empty;
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
