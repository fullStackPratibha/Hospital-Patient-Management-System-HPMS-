using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using HospitalManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Repositories;
using HospitalManagementAPI.Services;
using HospitalManagementAPI.Mappings;
using HospitalManagementAPI.Middleware;
using HospitalManagementAPI.Helpers;
using HospitalManagementAPI.Configurations;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",policy =>
    {
        policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings!.Issuer,
        ValidAudience = jwtSettings!.Audience,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token  = context.Request.Cookies["access_token"];
            context.Token = token;
            return Task.CompletedTask;
        }
    };
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


Log.Information("Hospital Management API Started Successfully.");
app.Run();

