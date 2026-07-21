using HospitalManagementAPI.DTOs.Auth;
using HospitalManagementAPI.Response;

namespace HospitalManagementAPI.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto request);
}