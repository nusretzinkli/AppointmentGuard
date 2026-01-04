using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Data.IRepositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient> GetByEmailAsync(string email);
        Task<Patient> GetByTcNoAsync(string tcNo);
    }
}