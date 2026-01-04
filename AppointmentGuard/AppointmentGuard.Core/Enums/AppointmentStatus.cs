using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentGuard.Core.Enums
{
    public enum AppointmentStatus
    {
        Available = 1,  
        Booked = 2,     
        Completed = 3, 
        Cancelled = 4,  
        NoShow = 5      
    }
}
