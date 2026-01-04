using AppointmentGuard.Core.Entities;
using AppointmentGuard.Core.Enums;
using AppointmentGuard.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AppointmentGuard.API
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                context.Database.Migrate();

                if (context.Polyclinics.Any())
                {
                    return;
                }

                var polyclinics = new List<Polyclinic>
                {
                    new Polyclinic { Name = "Dahiliye", Description = "İç organ hastalıkları tanı ve tedavisi.", LocationInfo = "A Blok, 1. Kat", ExaminationDurationMinutes = 15, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Polyclinic { Name = "Kardiyoloji", Description = "Kalp ve damar sağlığı.", LocationInfo = "A Blok, 2. Kat", ExaminationDurationMinutes = 20, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Polyclinic { Name = "Göz Hastalıkları", Description = "Göz muayenesi ve cerrahisi.", LocationInfo = "B Blok, Zemin", ExaminationDurationMinutes = 15, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Polyclinic { Name = "Ortopedi", Description = "Kas ve iskelet sistemi.", LocationInfo = "C Blok, 1. Kat", ExaminationDurationMinutes = 20, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Polyclinic { Name = "Kulak Burun Boğaz", Description = "KBB hastalıkları.", LocationInfo = "B Blok, 1. Kat", ExaminationDurationMinutes = 15, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Polyclinic { Name = "Nöroloji", Description = "Beyin ve sinir hastalıkları.", LocationInfo = "C Blok, 2. Kat", ExaminationDurationMinutes = 30, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Polyclinic { Name = "Dermatoloji", Description = "Cilt hastalıkları ve tedavi.", LocationInfo = "B Blok, 2. Kat", ExaminationDurationMinutes = 10, CreatedDate = DateTime.Now, IsDeleted = false }
                };

                context.Polyclinics.AddRange(polyclinics);
                context.SaveChanges();

                var doctors = new List<Doctor>
                {
                    new Doctor { FirstName = "Ahmet", LastName = "Yılmaz", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[0].Id, DiplomaNo = "DIP-1001", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Ayşe", LastName = "Demir", Title = DoctorTitle.Professor, PolyclinicId = polyclinics[0].Id, DiplomaNo = "DIP-1002", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Mehmet", LastName = "Kaya", Title = DoctorTitle.Docent, PolyclinicId = polyclinics[1].Id, DiplomaNo = "DIP-1003", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Fatma", LastName = "Çelik", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[1].Id, DiplomaNo = "DIP-1004", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Mustafa", LastName = "Öztürk", Title = DoctorTitle.Operator, PolyclinicId = polyclinics[2].Id, DiplomaNo = "DIP-1005", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Zeynep", LastName = "Aydın", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[2].Id, DiplomaNo = "DIP-1006", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Kemal", LastName = "Sunal", Title = DoctorTitle.Professor, PolyclinicId = polyclinics[2].Id, DiplomaNo = "DIP-1007", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Emre", LastName = "Yıldız", Title = DoctorTitle.Operator, PolyclinicId = polyclinics[3].Id, DiplomaNo = "DIP-1008", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Canan", LastName = "Arslan", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[3].Id, DiplomaNo = "DIP-1009", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Burak", LastName = "Şahin", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[4].Id, DiplomaNo = "DIP-1010", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Elif", LastName = "Koç", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[4].Id, DiplomaNo = "DIP-1011", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Serkan", LastName = "Polat", Title = DoctorTitle.Docent, PolyclinicId = polyclinics[5].Id, DiplomaNo = "DIP-1012", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Gamze", LastName = "Ünal", Title = DoctorTitle.Professor, PolyclinicId = polyclinics[5].Id, DiplomaNo = "DIP-1013", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Deniz", LastName = "Aksoy", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[6].Id, DiplomaNo = "DIP-1014", CreatedDate = DateTime.Now, IsDeleted = false },
                    new Doctor { FirstName = "Selin", LastName = "Yavuz", Title = DoctorTitle.Uzman, PolyclinicId = polyclinics[6].Id, DiplomaNo = "DIP-1015", CreatedDate = DateTime.Now, IsDeleted = false }
                };

                context.Doctors.AddRange(doctors);
                context.SaveChanges();

                string commonPasswordHash = BCrypt.Net.BCrypt.HashPassword("123456");

                var patients = new List<Patient>
                {
                    new Patient { FirstName = "Cemal", LastName = "Can", Email = "cemal@test.com", Phone = "05551112233", TcNo = "11111111110", PasswordHash = commonPasswordHash, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Patient { FirstName = "Merve", LastName = "Boluğur", Email = "merve@test.com", Phone = "05552223344", TcNo = "22222222220", PasswordHash = commonPasswordHash, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Patient { FirstName = "Hakan", LastName = "Taşıyan", Email = "hakan@test.com", Phone = "05553334455", TcNo = "33333333330", PasswordHash = commonPasswordHash, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Patient { FirstName = "Seda", LastName = "Sayan", Email = "seda@test.com", Phone = "05554445566", TcNo = "44444444440", PasswordHash = commonPasswordHash, CreatedDate = DateTime.Now, IsDeleted = false },
                    new Patient { FirstName = "Acun", LastName = "Ilıcalı", Email = "acun@test.com", Phone = "05555556677", TcNo = "55555555550", PasswordHash = commonPasswordHash, CreatedDate = DateTime.Now, IsDeleted = false }
                };

                context.Patients.AddRange(patients);
                context.SaveChanges();

                var appointments = new List<Appointment>();

                appointments.Add(new Appointment
                {
                    DoctorId = doctors[0].Id,
                    PatientId = patients[0].Id,
                    AppointmentDate = DateTime.Today.AddDays(1).AddHours(9).AddMinutes(0),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                appointments.Add(new Appointment
                {
                    DoctorId = doctors[0].Id,
                    PatientId = patients[1].Id,
                    AppointmentDate = DateTime.Today.AddDays(1).AddHours(9).AddMinutes(30),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                appointments.Add(new Appointment
                {
                    DoctorId = doctors[2].Id,
                    PatientId = patients[2].Id,
                    AppointmentDate = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(0),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                appointments.Add(new Appointment
                {
                    DoctorId = doctors[4].Id,
                    PatientId = patients[3].Id,
                    AppointmentDate = DateTime.Today.AddDays(2).AddHours(14).AddMinutes(0),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                appointments.Add(new Appointment
                {
                    DoctorId = doctors[7].Id,
                    PatientId = patients[4].Id,
                    AppointmentDate = DateTime.Today.AddDays(1).AddHours(11).AddMinutes(30),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                appointments.Add(new Appointment
                {
                    DoctorId = doctors[5].Id,
                    PatientId = patients[1].Id,
                    AppointmentDate = DateTime.Today.AddDays(3).AddHours(16).AddMinutes(0),
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                context.Appointments.AddRange(appointments);
                context.SaveChanges();
            }
        }
    }
}