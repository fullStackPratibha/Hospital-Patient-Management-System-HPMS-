using HospitalManagementAPI.Enums;

namespace HospitalManagementAPI.DTOs;

public class PatientProfileDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public GenderType Gender { get; set; }
    public string Address { get; set; } = string.Empty;
}
