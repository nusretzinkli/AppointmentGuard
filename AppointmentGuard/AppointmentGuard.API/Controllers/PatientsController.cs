using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;
        private readonly IMapper _mapper;

        public PatientsController(IPatientService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<PatientDto>(patient));
        }
    }
}