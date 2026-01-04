using AppointmentGuard.Core.Entities;

namespace AppointmentGuard.Service.Interfaces
{
    public interface IPolyclinicService : IService<Polyclinic>
    {
        Task<Polyclinic> GetPolyclinicWithDoctorsAsync(int id);
        Task<List<Polyclinic>> GetAllWithDoctorsAsync();
    }
}