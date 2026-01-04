using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppointmentGuard.Core.Enums;

namespace AppointmentGuard.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string TcNo { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; } 

        public bool IsBlocked { get; set; } = false;

        public ICollection<Appointment> Appointments { get; set; }
    }
}
