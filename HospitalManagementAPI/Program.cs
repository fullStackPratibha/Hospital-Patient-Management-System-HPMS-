using HospitalManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Repositories;
using HospitalManagementAPI.Services;
using HospitalManagementAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddAutoMapper(typeof(PatientProfile));

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();

