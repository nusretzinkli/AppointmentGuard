using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Service.Interfaces;

namespace AppointmentGuard.Service.Implementations
{
    public class PolyclinicService : Service<Polyclinic>, IPolyclinicService
    {
        private readonly IPolyclinicRepository _polyclinicRepository;

        public PolyclinicService(IGenericRepository<Polyclinic> repository, IUnitOfWork unitOfWork, IPolyclinicRepository polyclinicRepository)
            : base(repository, unitOfWork)
        {
            _polyclinicRepository = polyclinicRepository;
        }

        public async Task<Polyclinic> GetPolyclinicWithDoctorsAsync(int id)
        {
            return await _polyclinicRepository.GetPolyclinicWithDoctorsAsync(id);
        }

        public async Task<List<Polyclinic>> GetAllWithDoctorsAsync()
        {
            return await _polyclinicRepository.GetAllWithDoctorsAsync();
        }
    }
}