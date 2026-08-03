using HospitalManagementAPI.Entities;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Interfaces;
using HospitalManagementAPI.Exceptions;
using AutoMapper;

namespace HospitalManagementAPI.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }
    public async Task<List<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return _mapper.Map<List<PatientDto>>(patients);
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
    {
        var existingPatient = await _patientRepository.GetByPhoneAsync(dto.PhoneNumber);

        if (existingPatient != null)
        {
            throw new DuplicatePhoneException("Phone number already exists.");
        }

        var patient = _mapper.Map<Patient>(dto);

        await _patientRepository.AddAsync(patient);
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            throw new PatientNotFoundException($"Patient with ID {id} not found.");
        }
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePatientDto dto)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            return false;
        }


        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.Address = dto.Address;

        await _patientRepository.UpdateAsync(patient);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _patientRepository.DeleteAsync(id);
    }

}