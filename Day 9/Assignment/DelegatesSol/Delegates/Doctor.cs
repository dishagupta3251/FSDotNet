using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement
{
    internal class Doctor : Register
    {
        int ID_Doctor;
        string nameDoctor { get; set; }
        string emailDoctor { get; set; }
        string phoneDoctor { get; set; }
       
        public Doctor(){
            TakeInputFromConsole();
            ID_Doctor = ID;
            nameDoctor= Name;
            emailDoctor= Email;
            phoneDoctor=PhoneNo;
        }

       
        public override string ToString()
        {
            return "Doctor"+ID_Doctor+" "+nameDoctor+" "+emailDoctor+" "+phoneDoctor;
        }
    }
}
