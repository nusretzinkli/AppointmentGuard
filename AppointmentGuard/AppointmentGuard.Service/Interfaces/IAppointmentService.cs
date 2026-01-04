using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Service.Interfaces
{
    public interface IAppointmentService : IService<Appointment>
    {
        Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto dto);
        Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId, DateTime date);
    }
}