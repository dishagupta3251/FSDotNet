using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClinicManagement
{
    internal class Patients:Register
    {
        int ID_Patients = 0;
        string namePatients = "";
        string emailPatients = "";
        string phonePatients = "";
        public void CallForInputPatients()
        {
            TakeInputFromConsole();
            ID_Patients = ID;
            namePatients = Name;
            emailPatients = Email;
            phonePatients = PhoneNo;
        }
        public override string ToString()
        {
            return "Doctor" + ID_Patients + " " + namePatients + " " + emailPatients + " " + phonePatients;
        }
    }
}
