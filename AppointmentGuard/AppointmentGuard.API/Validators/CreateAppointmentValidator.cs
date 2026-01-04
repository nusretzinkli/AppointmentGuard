using AppointmentGuard.Core.DTOs; 
using FluentValidation;

namespace AppointmentGuard.API.Validators
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.DoctorId)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir doktor seçiniz.");

            RuleFor(x => x.PatientId)
                .GreaterThan(0).WithMessage("Hasta bilgisi eksik.");

            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("Randevu tarihi boş olamaz.")
                .GreaterThan(DateTime.Now).WithMessage("Geçmiş bir tarihe randevu alamazsınız.");
        }
    }
}