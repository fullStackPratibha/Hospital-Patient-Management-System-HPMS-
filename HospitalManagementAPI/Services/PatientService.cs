using HospitalManagementAPI.Models;
using HospitalManagementAPI.DTOs;
using HospitalManagementAPI.Interfaces;
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
            var patient = _mapper.Map<Patient>(dto);

            await _patientRepository.AddAsync(patient);
            return _mapper.Map<PatientDto>(patient);
        }
    }