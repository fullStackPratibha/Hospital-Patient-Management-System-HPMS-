namespace HospitalManagementAPI.DTOs;

public class PatientDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

     public string Gender { get; set; } = string.Empty;
}