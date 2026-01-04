using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Service.Interfaces
{
    public interface IDoctorService : IService<Doctor>
    {
        Task<List<Doctor>> GetDoctorsWithPolyclinicAsync();
    }
}