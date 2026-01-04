using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppointmentGuard.Core.Enums;

namespace AppointmentGuard.Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{Title} {FirstName} {LastName}"; 

        public DoctorTitle Title { get; set; } 

        public string DiplomaNo { get; set; } 

        public int PolyclinicId { get; set; }
        public Polyclinic Polyclinic { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}