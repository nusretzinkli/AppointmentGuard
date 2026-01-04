using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _service.GetDoctorsWithPolyclinicAsync();
            var doctorsDto = _mapper.Map<List<DoctorDto>>(doctors);

            return Ok(doctorsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            if (doctor == null) return NotFound("Doktor bulunamadı.");

            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Save(DoctorDto doctorDto) 
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            var newDoctor = await _service.AddAsync(doctor);

            return CreatedAtAction(nameof(GetById), new { id = newDoctor.Id }, newDoctor);
        }

        [HttpGet("GetByPolyclinic/{polyclinicId}")]
        public async Task<IActionResult> GetByPolyclinic(int polyclinicId)
        {
            var allDoctors = await _service.GetAllAsync(); 
            var filteredDoctors = allDoctors.Where(x => x.PolyclinicId == polyclinicId).ToList();

            return Ok(filteredDoctors);
        }
    }
}