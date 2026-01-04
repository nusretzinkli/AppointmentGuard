using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Data.IRepositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<List<Doctor>> GetDoctorsWithPolyclinicAsync();
        Task<Doctor> GetDoctorWithPolyclinicByIdAsync(int doctorId);
    }
}