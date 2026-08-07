using HospitalManagementAPI.Entities;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Exceptions;
using AutoMapper;

using HospitalManagementAPI.Enums;

namespace HospitalManagementAPI.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public PatientService(
        IPatientRepository patientRepository, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }
    public async Task<List<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllPatientsAsync();
        return _mapper.Map<List<PatientDto>>(patients);
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto dto)
    {
        var existingPatient = await _patientRepository.GetPatientByPhoneAsync(dto.PhoneNumber);

        if (existingPatient != null)
        {
            throw new DuplicatePhoneException("Phone number already exists.");
        }

        var existingEmail = await _userRepository.UserEmailExistsAsync(dto.Email);
        if (existingEmail)
        {
            throw new DuplicateEmailException("Email already exists.");
        }

        _passwordHasher.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            FullName = $"{dto.FirstName} {dto.LastName}".Trim(),
            Email = dto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = UserRole.Patient,
            IsActive = true
        };

        var patient = _mapper.Map<Patient>(dto);
        user.Patient = patient;

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveUserChangesAsync();

        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientProfileDto?> GetPatientByUserIdAsync(int userId)
    {
        var patient = await _patientRepository
            .GetPatientByUserIdAsync(userId);

        if (patient == null)
        {
            return null;
        }
        return _mapper.Map<PatientProfileDto>(patient);
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        if (patient == null)
        {
            throw new PatientNotFoundException($"Patient with ID {id} not found.");
        }
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<bool> UpdatePatientAsync(int id, UpdatePatientDto dto)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return false;
        }


        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.Address = dto.Address;

        if (patient.User != null)
        {
            patient.User.FullName = $"{dto.FirstName} {dto.LastName}".Trim();
        }

        await _patientRepository.UpdatePatientAsync(patient);
        return true;
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        return await _patientRepository.DeletePatientAsync(id);
    }

}