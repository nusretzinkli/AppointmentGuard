using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Service.Interfaces;

namespace AppointmentGuard.Service.Implementations
{
    public class DoctorService : Service<Doctor>, IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IGenericRepository<Doctor> repository, IUnitOfWork unitOfWork, IDoctorRepository doctorRepository)
            : base(repository, unitOfWork)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<Doctor>> GetDoctorsWithPolyclinicAsync()
        {
            return await _doctorRepository.GetDoctorsWithPolyclinicAsync();
        }
    }
}