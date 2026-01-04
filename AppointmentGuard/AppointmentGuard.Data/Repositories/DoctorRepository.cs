using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AppointmentGuard.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Doctor>> GetDoctorsWithPolyclinicAsync()
        {
            return await _context.Doctors.Include(x => x.Polyclinic).ToListAsync();
        }

        public async Task<Doctor> GetDoctorWithPolyclinicByIdAsync(int doctorId)
        {
            return await _context.Doctors
                .Include(x => x.Polyclinic)
                .FirstOrDefaultAsync(x => x.Id == doctorId);
        }
    }
}