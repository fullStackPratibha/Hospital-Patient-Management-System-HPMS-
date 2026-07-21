using HospitalManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Repositories;
using HospitalManagementAPI.Services;
using HospitalManagementAPI.Mappings;
using HospitalManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddAutoMapper(typeof(PatientProfile));

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

