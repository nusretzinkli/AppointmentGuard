using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AppointmentGuard.Data.Repositories
{
    public class PolyclinicRepository : GenericRepository<Polyclinic>, IPolyclinicRepository
    {
        public PolyclinicRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Polyclinic> GetPolyclinicWithDoctorsAsync(int id)
        {
            return await _context.Polyclinics
                .Include(x => x.Doctors)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Polyclinic>> GetAllWithDoctorsAsync()
        {
            return await _context.Polyclinics
                .Include(x => x.Doctors) 
                .ToListAsync();
        }
    }
}