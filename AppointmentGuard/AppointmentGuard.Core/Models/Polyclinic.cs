using System.Collections.Generic;

namespace AppointmentGuard.Core.Entities
{
    public class Polyclinic : BaseEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocationInfo { get; set; }
        public int ExaminationDurationMinutes { get; set; } = 15;
        public ICollection<Doctor> Doctors { get; set; }
    }
}