using AppointmentGuard.Core.Entities;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Service.Interfaces;

namespace AppointmentGuard.Service.Implementations
{
    public class PatientService : Service<Patient>, IPatientService
    {
        public PatientService(IGenericRepository<Patient> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}