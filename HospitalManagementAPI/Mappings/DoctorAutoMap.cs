using AutoMapper;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Mappings;

public class DoctorAutoMap : Profile
{
    public DoctorAutoMap()
    {
        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
        CreateMap<Doctor, DoctorProfileDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
    }
}
