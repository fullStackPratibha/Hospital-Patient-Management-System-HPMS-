using HospitalManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Repositories;
using HospitalManagementAPI.Services;
using HospitalManagementAPI.Mappings;
using HospitalManagementAPI.Middleware;
using HospitalManagementAPI.Helpers;
using HospitalManagementAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddAutoMapper(typeof(PatientProfile));

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IJwtTokenGenerator,JwtTokenGenerator>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

