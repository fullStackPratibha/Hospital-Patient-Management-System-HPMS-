using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Response;
using HospitalManagementAPI.Enums;

namespace HospitalManagementAPI.Services;

public class AuthService(
    IUserRepository userRepository,
    IPatientRepository patientRepository,
    IPasswordHasher passwordHasher) : IAuthService
{
    public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto requestDto)
    {
        requestDto.Email = requestDto.Email.Trim().ToLower();
        if(await userRepository.EmailExitsAsync(requestDto.Email))
        {
            throw new Exception("Email already Exists.");
        }
        passwordHasher.CreatePasswordHash(
            requestDto.Password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        );

        var user = new User
        {
            Email = requestDto.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = UserRole.Patient,
            IsActive = true
        };

        var patient = new Patient
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            PhoneNumber = requestDto.Phone,
            Email = requestDto.Email,
            Gender = requestDto.Gender,
            DateOfBirth = requestDto.DateOfBirth,
            Address = requestDto.Address,
            IsDeleted = false
        };

        user.Patient = patient;

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();
    
        return new ApiResponse<string>(
        true,
        StatusCodes.Status201Created,
        "Register logic created successfully.",
         $"User ID: {user.Id}"
        );
    }
}