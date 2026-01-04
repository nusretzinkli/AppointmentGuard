using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Service.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto loginDto);
        Task<Patient> RegisterAsync(RegisterDto registerDto);
    }
}