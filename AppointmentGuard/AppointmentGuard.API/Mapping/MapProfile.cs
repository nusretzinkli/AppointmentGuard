using AutoMapper;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Core.DTOs;

namespace AppointmentGuard.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.ToString()))
                .ForMember(dest => dest.PolyclinicName, opt => opt.MapFrom(src => src.Polyclinic.Name));
            CreateMap<Polyclinic, PolyclinicDto>();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FirstName + " " + src.Doctor.LastName))
                .ForMember(dest => dest.PolyclinicName, opt => opt.MapFrom(src => src.Doctor.Polyclinic.Name));
            CreateMap<CreateAppointmentDto, Appointment>();
        }
    }
}