﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorEF_Project.Models
{
    internal class Doctor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public string? Phone {  get; set; }
        public string? Email { get; set; }
    }
}
