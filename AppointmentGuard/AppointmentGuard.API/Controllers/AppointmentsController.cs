using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Save(CreateAppointmentDto dto)
        {
            try
            {
                var newAppointment = await _service.CreateAppointmentAsync(dto);
                return Ok(newAppointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Randevu oluşturulurken beklenmedik bir hata oluştu." });
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId, [FromQuery] DateTime date)
        {
            if (date == DateTime.MinValue) date = DateTime.Now;

            var appointments = await _service.GetDoctorAppointmentsAsync(doctorId, date);
            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            return Ok(appointmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<AppointmentDto>(appointment));
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var appointments = await _service.Where(x => x.PatientId == patientId)
                                             .Include(x => x.Doctor)
                                             .ThenInclude(x => x.Polyclinic)
                                             .ToListAsync();

            var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);
            return Ok(appointmentDtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var appointment = await _service.GetByIdAsync(id);

            if (appointment == null)
                return NotFound("Randevu bulunamadı.");

            await _service.RemoveAsync(appointment);

            return NoContent();
        }
    }
}