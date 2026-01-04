using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppointmentGuard.Core.Enums;

namespace AppointmentGuard.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int? PatientId { get; set; } 
        public Patient? Patient { get; set; }

        public DateTime AppointmentDate { get; set; } 

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Available;

        public string? PatientNote { get; set; }

        public string? CancellationReason { get; set; }
    }
}
