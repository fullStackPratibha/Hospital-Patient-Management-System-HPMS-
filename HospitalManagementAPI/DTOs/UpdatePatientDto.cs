using System.ComponentModel.DataAnnotations;

namespace HospitalManagementAPI.DTOs;

public class UpdatePatientDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

}