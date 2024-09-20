using HospitalWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApplication.Controllers
{
    public class DoctorController : Controller
    {
        List<Doctor> doctors = new List<Doctor>() {
            new Doctor{Id=101,Name="Disha",Phone="3672836481",Speciality="Cardiologist", Experience=5,Image="/images/d1.jpg"},
            new Doctor{Id=102,Name="XYZ",Phone="3672838495",Speciality="Dentist", Experience=2,Image="/images/d2.jpg"},
            new Doctor{Id=103,Name="ABC",Phone="4928384958",Speciality="Eye Specialist", Experience=4,Image="/images/d3.jpg"},
            new Doctor{Id=104,Name="John",Phone="4928384998",Speciality="Neurologist", Experience=3,Image="/images/d4.jpg"}
        };

        public IActionResult Index()
        {
            return View(doctors);
        }
    }
}
