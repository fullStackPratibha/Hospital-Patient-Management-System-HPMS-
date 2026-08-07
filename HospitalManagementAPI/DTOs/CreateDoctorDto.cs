using System.ComponentModel.DataAnnotations;

namespace HospitalManagementAPI.DTOs;

public class CreateDoctorDto
{
    [Required(ErrorMessage = "Full Name is required.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specialization is required.")]
    public string Specialization { get; set; } = string.Empty;

    [Range(0, 70, ErrorMessage = "Experience must be between 0 and 70 years.")]
    public int ExperienceYears { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string PhoneNumber { get; set; } = string.Empty;
}
