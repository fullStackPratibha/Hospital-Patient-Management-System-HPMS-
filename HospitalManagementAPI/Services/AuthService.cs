using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Enums;
using HospitalManagementAPI.Exceptions;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Models.Auth;
using HospitalManagementAPI.Response;

namespace HospitalManagementAPI.Services;

public class AuthService(
    IUserRepository userRepository,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository,
    IPasswordHasher passwordHasher, 
    IJwtTokenGenerator jwtTokenGenerator,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto requestDto)
    {
        requestDto.Email = requestDto.Email.Trim().ToLower();
        if(await userRepository.UserEmailExistsAsync(requestDto.Email))
        {
            throw new DuplicateEmailException("Email already Exists.");
        }
        passwordHasher.CreatePasswordHash(
            requestDto.Password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        );

        var user = new User
        {
            FullName = $"{requestDto.FirstName} {requestDto.LastName}".Trim(),
            Email = requestDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = requestDto.Role,
            IsActive = true
        };

        if (requestDto.Role == UserRole.Doctor)
        {
            var existingDoctorPhone = await doctorRepository.GetDoctorByPhoneAsync(requestDto.Phone);
            if (existingDoctorPhone != null)
            {
                throw new DuplicatePhoneException("Doctor phone number already exists.");
            }

            var doctor = new Doctor
            {
                Specialization = requestDto.Specialization ?? string.Empty,
                ExperienceYears = requestDto.ExperienceYears ?? 0,
                PhoneNumber = requestDto.Phone
            };
            user.Doctor = doctor;
        }
        else
        {
            var existingPatientPhone = await patientRepository.GetPatientByPhoneAsync(requestDto.Phone);
            if (existingPatientPhone != null)
            {
                throw new DuplicatePhoneException("Patient phone number already exists.");
            }

            var patient = new Patient
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                PhoneNumber = requestDto.Phone,
                Gender = requestDto.Gender,
                DateOfBirth = requestDto.DateOfBirth,
                Address = requestDto.Address,
                IsDeleted = false
            };
            user.Patient = patient;
        }

        await userRepository.AddUserAsync(user);
        await userRepository.SaveUserChangesAsync();
        logger.LogInformation(
    "User registered successfully. UserId: {UserId}, Email: {Email}, Role: {Role}",
    user.Id,
    user.Email,
    user.Role);

        return new ApiResponse<string>(
        true,
        StatusCodes.Status201Created,
        "User registered successfully.",
        $"User Id {user.Id}"
        );
    }

    public async Task<LoginResult> LoginAsync(LoginRequestDto requestDto)
    {
        requestDto.Email = requestDto.Email.Trim().ToLower();

        var user = await userRepository.GetUserByEmailAsync( requestDto.Email );
        if(user == null)
        {
            logger.LogWarning(
    "Login failed. Email not found: {Email}",
    requestDto.Email);
            throw new InvalidCredentialException("Invalid Email or Password.");
        }
        bool isvalidPassword = passwordHasher.VerifyPasswordHash(
            requestDto.Password,
            user.PasswordHash,
            user.PasswordSalt);

        if(!isvalidPassword)
        {
            logger.LogWarning("Invalid password for Email: {Email}",requestDto.Email);
            throw new InvalidCredentialException("Invalid Email or Password.");
        }
        string token = jwtTokenGenerator.GenerateToken(user);
        logger.LogInformation(
    "User logged in successfully. UserId: {UserId}, Email: {Email}",
    user.Id,
    user.Email);

        return new LoginResult
        {
            Token = token,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }

}