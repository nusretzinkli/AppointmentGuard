using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Data.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end);
        Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId, DateTime date);
    }
}