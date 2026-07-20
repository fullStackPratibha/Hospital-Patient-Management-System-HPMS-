using AutoMapper;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Models;

namespace HospitalManagementAPI.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.FullName, 
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
    }
}