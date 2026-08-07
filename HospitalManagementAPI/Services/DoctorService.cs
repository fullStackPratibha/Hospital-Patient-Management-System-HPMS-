using HospitalManagementAPI.Entities;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Exceptions;
using HospitalManagementAPI.Enums;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagementAPI.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public DoctorService(
        IDoctorRepository doctorRepository, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<List<DoctorDto>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorRepository.GetAllDoctorsAsync();
        return _mapper.Map<List<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
        if (doctor == null) return null;
        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<DoctorProfileDto?> GetDoctorByUserIdAsync(int userId)
    {
        var doctor = await _doctorRepository.GetDoctorByUserIdAsync(userId);
        if (doctor == null) return null;
        return _mapper.Map<DoctorProfileDto>(doctor);
    }

    public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto dto)
    {
        var existingPhone = await _doctorRepository.GetDoctorByPhoneAsync(dto.PhoneNumber);
        if (existingPhone != null)
        {
            throw new DuplicatePhoneException("Doctor phone number already exists.");
        }

        var existingEmail = await _userRepository.UserEmailExistsAsync(dto.Email);
        if (existingEmail)
        {
            throw new DuplicateEmailException("Email already exists.");
        }

        _passwordHasher.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = UserRole.Doctor,
            IsActive = true
        };

        var doctor = _mapper.Map<Doctor>(dto);
        user.Doctor = doctor;

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveUserChangesAsync();

        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<bool> UpdateDoctorAsync(int id, UpdateDoctorDto dto)
    {
        var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
        if (doctor == null) return false;

        if (dto.Specialization != null)
        {
            doctor.Specialization = dto.Specialization;
        }
        if (dto.ExperienceYears.HasValue)
        {
            doctor.ExperienceYears = dto.ExperienceYears.Value;
        }
        if (dto.PhoneNumber != null && dto.PhoneNumber != doctor.PhoneNumber)
        {
            var existingPhone = await _doctorRepository.GetDoctorByPhoneAsync(dto.PhoneNumber);
            if (existingPhone != null)
            {
                throw new DuplicatePhoneException("Doctor phone number already exists.");
            }
            doctor.PhoneNumber = dto.PhoneNumber;
        }

        await _doctorRepository.UpdateDoctorAsync(doctor);
        return true;
    }

    public async Task<bool> DeleteDoctorAsync(int id)
    {
        return await _doctorRepository.DeleteDoctorAsync(id);
    }
}
