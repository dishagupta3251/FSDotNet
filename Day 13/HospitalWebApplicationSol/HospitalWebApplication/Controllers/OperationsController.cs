using System.Numerics;
using HospitalWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWebApplication.Controllers
{
    public class OperationsController:Controller
    {
       private static List<Doctor> doctors = new List<Doctor>() {
            new Doctor{Id=101,Name="Disha",Phone="3672836481",Speciality="Cardiologist", Experience=5,Image="/images/d1.jpg"},
            new Doctor{Id=102,Name="XYZ",Phone="3672838495",Speciality="Dentist", Experience=2,Image="/images/d2.jpg"},
            new Doctor{Id=103,Name="ABC",Phone="4928384958",Speciality="Eye Specialist", Experience=4,Image="/images/d3.jpg"},
            new Doctor{Id=104,Name="John",Phone="4928384998",Speciality="Neurologist", Experience=3,Image="/images/d4.jpg"}
        };

        public IActionResult HomePage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewAllDoctors()
        {
            return View(doctors);
        }
        [HttpGet]
        public IActionResult Add_Doctors()
        {
            Doctor doctor = new Doctor();
            return View(doctor);
        }
        [HttpPost]

        public IActionResult Add_Doctor(Doctor doctor)
        {
            doctor.Id = doctors.Count + 1;
            doctor.Image = "/images/" + doctor.Image;
            doctors.Add(doctor);
            return RedirectToAction("ViewAllDoctors");
            
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditDoctor(int pid, Doctor doctor)
        {
            Doctor oldDoctor = doctors.FirstOrDefault(p => p.Id == pid);
            oldDoctor.Phone = doctor.Phone;
            oldDoctor.Speciality = doctor.Speciality;
            oldDoctor.Experience = doctor.Experience;
            
            return RedirectToAction("ViewAllDoctors");
        }
        [HttpGet]
        public IActionResult DeleteDoctor(int pid)
        {
            Doctor doctor = doctors.FirstOrDefault(p => p.Id == pid);
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Delete(int pid, Doctor doctor)
        {
            Doctor oldDoctor = doctors.FirstOrDefault(p => p.Id == pid);
            doctors.Remove(oldDoctor);
            return RedirectToAction("ViewAllDoctors");
        }
    }
}
