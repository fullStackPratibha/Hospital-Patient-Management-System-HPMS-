using System.ComponentModel.DataAnnotations;

namespace HospitalManagementAPI.DTOs;

public class UpdateDoctorDto
{
    public string? Specialization { get; set; }

    [Range(0, 70, ErrorMessage = "Experience must be between 0 and 70 years.")]
    public int? ExperienceYears { get; set; }

    [Phone(ErrorMessage = "Invalid phone number.")]
    public string? PhoneNumber { get; set; }
}
