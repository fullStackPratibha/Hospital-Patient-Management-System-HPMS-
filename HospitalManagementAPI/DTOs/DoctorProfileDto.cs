namespace HospitalManagementAPI.DTOs;

public class DoctorProfileDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}
