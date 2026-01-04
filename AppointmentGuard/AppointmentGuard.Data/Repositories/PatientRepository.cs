using System;
using System.Collections.Generic;
using System.Linq;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AppointmentGuard.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Patient> GetByEmailAsync(string email)
        {
            return await _context.Patients.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Patient> GetByTcNoAsync(string tcNo)
        {
            return await _context.Patients.FirstOrDefaultAsync(x => x.TcNo == tcNo);
        }
    }
}