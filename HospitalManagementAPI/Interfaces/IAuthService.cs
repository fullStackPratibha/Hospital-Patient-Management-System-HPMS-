using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Response;
using HospitalManagementAPI.Models.Auth;

namespace HospitalManagementAPI.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto request);
    Task<LoginResult> LoginAsync(LoginRequestDto requestDto);
}