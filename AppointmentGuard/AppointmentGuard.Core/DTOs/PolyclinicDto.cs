namespace AppointmentGuard.Core.DTOs
{
    public class PolyclinicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocationInfo { get; set; }
        public int ExaminationDurationMinutes { get; set; }
        public List<DoctorDto> Doctors { get; set; }
    }
}