using HospitalManagementAPI.Entities;

namespace HospitalManagementAPI.Interfaces;

public interface IPasswordHasher
{
    void CreatePasswordHash(
        string password,
        out byte[] passwordHash,
        out byte[] passwordSalt);

    bool VerifyPasswordHash(
        string password,
        byte[] passwordHash,
        byte[] passwordSalt);
}