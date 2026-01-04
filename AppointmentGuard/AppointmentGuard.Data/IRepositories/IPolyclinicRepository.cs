using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Data.IRepositories
{
    public interface IPolyclinicRepository : IGenericRepository<Polyclinic>
    {
        Task<Polyclinic> GetPolyclinicWithDoctorsAsync(int id);
        Task<List<Polyclinic>> GetAllWithDoctorsAsync();
    }
}