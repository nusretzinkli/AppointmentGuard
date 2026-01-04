using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Core.Enums;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Service.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentGuard.Service.Implementations
{
    public class AppointmentService : Service<Appointment>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IGenericRepository<Appointment> repository, IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository, IMapper mapper)
            : base(repository, unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto dto)
        {
            if (dto.AppointmentDate < DateTime.Now)
                throw new InvalidOperationException("Geçmiş zamana randevu alınamaz.");

            var isSlotTaken = await _appointmentRepository
                .Where(x => x.DoctorId == dto.DoctorId && x.AppointmentDate == dto.AppointmentDate)
                .AnyAsync();

            if (isSlotTaken)
            {
                throw new InvalidOperationException("Seçilen saatte doktorun başka bir randevusu mevcut. Lütfen sayfayı yenileyip başka bir saat seçiniz.");
            }

            var appointment = _mapper.Map<Appointment>(dto);
            appointment.CreatedDate = DateTime.Now;
            appointment.Status = AppointmentStatus.Booked;

            try
            {
                await _appointmentRepository.AddAsync(appointment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                var checkAgain = await _appointmentRepository
                    .Where(x => x.DoctorId == dto.DoctorId && x.AppointmentDate == dto.AppointmentDate)
                    .AnyAsync();

                if (checkAgain)
                {
                    throw new InvalidOperationException("Üzgünüz, bu randevu işlem sırasında başkası tarafından alındı.");
                }

                throw;
            }

            return appointment;
        }

        public async Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId, DateTime date)
        {
            return await _appointmentRepository.GetDoctorAppointmentsAsync(doctorId, date);
        }
    }
}