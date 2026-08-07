using HospitalManagementAPI.Enums;
using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Entities;

public class User
{
    public int Id {get; set;}
    public string FullName { get; set; } = string.Empty;
    public string Email {get; set;} = string.Empty;
    public byte[] PasswordHash { get; set;} = [];
    public byte[] PasswordSalt {get; set;} = [];
    public UserRole Role {get; set;}
    public bool IsActive {get; set;} = true;

    public DateTime CreatedAt{get; set;} = DateTime.UtcNow;

    public Patient? Patient{get;set;}
    public Doctor? Doctor{get;set;}
}