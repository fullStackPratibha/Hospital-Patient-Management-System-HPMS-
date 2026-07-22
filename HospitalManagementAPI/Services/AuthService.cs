using Azure;
using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Enums;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Response;

namespace HospitalManagementAPI.Services;

public class AuthService(
    IUserRepository userRepository,
    IPatientRepository patientRepository,
    IPasswordHasher passwordHasher, 
    IJwtTokenGenerator jwtTokenGenerator) : IAuthService
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
        $"User Id {user.Id}"
        );
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto requestDto)
    {
        requestDto.Email = requestDto.Email.Trim().ToLower();

        var user = await userRepository.GetByEmailAsync( requestDto.Email );
        if(user == null)
        {
            throw new Exception("Invalid Email.");
        }
        bool isvalidPassword = passwordHasher.VerifyPasswordHash(
            requestDto.Password,
            user.PasswordHash,
            user.PasswordSalt);

        if(!isvalidPassword)
        {
            throw new Exception("Invalid Password");
        }
        string token = jwtTokenGenerator.GenerateToken(user);

        var response = new LoginResponseDto
        {
            Token = token,
            Email = user.Email,
            Role = user.Role.ToString()
        };
        return new ApiResponse<LoginResponseDto>(
            true,
            StatusCodes.Status200OK,
            "Login Successful",
            response
            );
    }

}