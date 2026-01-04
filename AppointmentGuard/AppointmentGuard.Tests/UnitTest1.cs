using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Core.Enums;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Service.Implementations;
using AppointmentGuard.Service.Interfaces;
using AutoMapper;
using MockQueryable.Moq; 
using Moq;
using Xunit;

namespace AppointmentGuard.Tests
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IGenericRepository<Appointment>> _mockGenericRepo;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _mockRepo = new Mock<IAppointmentRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockGenericRepo = new Mock<IGenericRepository<Appointment>>();

            _service = new AppointmentService(
                _mockGenericRepo.Object,
                _mockUnitOfWork.Object,
                _mockRepo.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task CreateAppointmentAsync_RaceCondition_ShouldThrowUserFriendlyException()
        {

            var dto = new CreateAppointmentDto
            {
                DoctorId = 1,
                AppointmentDate = DateTime.Now.AddDays(1) 
            };

            var emptyList = new List<Appointment>(); 
            var takenList = new List<Appointment> { new Appointment() }; 


            _mockRepo.SetupSequence(x => x.Where(It.IsAny<System.Linq.Expressions.Expression<Func<Appointment, bool>>>()))
                .Returns(emptyList.AsQueryable().BuildMock()) 
                .Returns(takenList.AsQueryable().BuildMock());

            _mockMapper.Setup(m => m.Map<Appointment>(dto)).Returns(new Appointment());

            _mockUnitOfWork.Setup(u => u.CommitAsync())
                .ThrowsAsync(new Microsoft.EntityFrameworkCore.DbUpdateException());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.CreateAppointmentAsync(dto)
            );

            Assert.Equal("Üzgünüz, bu randevu işlem sırasında başkası tarafından alındı.", exception.Message);
        }
    }
}