using System;

namespace HospitalManagementAPI.Entities;

public class Prescription
{
    public int Id { get; set; }
    
    public int RecordId { get; set; }
    public MedicalRecord MedicalRecord { get; set; } = null!;
    
    public string? Medicines { get; set; }
    public DateTime? FollowUpDate { get; set; }
}
