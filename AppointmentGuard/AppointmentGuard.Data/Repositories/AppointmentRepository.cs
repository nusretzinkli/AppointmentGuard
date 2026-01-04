using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AppointmentGuard.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Appointments
                .Where(x => x.AppointmentDate >= start && x.AppointmentDate <= end)
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId, DateTime date)
        {
            return await _context.Appointments
                .Where(x => x.DoctorId == doctorId
                            && x.AppointmentDate.Date == date.Date
                            && !x.IsDeleted) 
                .ToListAsync();
        }
    }
}