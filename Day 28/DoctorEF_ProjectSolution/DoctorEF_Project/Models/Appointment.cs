using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorEF_Project.Models
{
    internal class Appointment
    {
        public int AppointmentId { get; set; }
        //[ForeignKey]
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
