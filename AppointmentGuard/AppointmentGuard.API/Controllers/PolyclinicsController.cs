using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Service.Interfaces;
using AutoMapper; 
using Microsoft.AspNetCore.Mvc;

namespace AppointmentGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicsController : ControllerBase
    {
        private readonly IPolyclinicService _service;
        private readonly IMapper _mapper; 

        public PolyclinicsController(IPolyclinicService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var polyclinics = await _service.GetAllWithDoctorsAsync();
            var polyclinicsDto = _mapper.Map<List<PolyclinicDto>>(polyclinics);

            return Ok(polyclinicsDto);
        }

        [HttpGet("{id}/doctors")]
        public async Task<IActionResult> GetWithDoctors(int id)
        {
            var polyclinic = await _service.GetPolyclinicWithDoctorsAsync(id);
            return Ok(_mapper.Map<PolyclinicDto>(polyclinic));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Polyclinic polyclinic)
        {
            await _service.AddAsync(polyclinic);
            return Ok(polyclinic);
        }
    }
}