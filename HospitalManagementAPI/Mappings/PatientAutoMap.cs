using AutoMapper;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Mappings;

public class PatientAutoMap : Profile
{
    public PatientAutoMap()
    {
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.FullName, 
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
            );
        CreateMap<Patient, PatientProfileDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
    }
}
