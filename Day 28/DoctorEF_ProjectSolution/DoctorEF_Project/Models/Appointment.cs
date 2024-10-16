using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DoctorEF_Project.Models
{
 public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
       




    }
}

