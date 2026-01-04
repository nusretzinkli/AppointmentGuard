using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
            {
                return BadRequest(new { message = "E-posta adresi veya şifre hatalı." });
            }

            return Ok(result);
        }
    }
}