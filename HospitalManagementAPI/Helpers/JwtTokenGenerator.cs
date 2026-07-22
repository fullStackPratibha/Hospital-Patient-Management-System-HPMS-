using HospitalManagementAPI.Configurations;
using HospitalManagementAPI.Entities;
using HospitalManagementAPI.Interfaces;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace HospitalManagementAPI.Helpers
{
    public class JwtTokenGenerator: IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Role,user.Role.ToString())
           };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
                claims:claims,
                expires:DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials:credentials);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
