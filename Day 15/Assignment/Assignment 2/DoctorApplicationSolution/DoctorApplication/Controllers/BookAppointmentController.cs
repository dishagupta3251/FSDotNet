using DoctorApplication.Interfaces;
using DoctorApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApplication.Controllers
{
    public class BookAppointmentController : Controller
    {
        private readonly IBookAppointmentServices _appointmentServices;
        private readonly IRepository<int, Doctor> _doctorRepository;

        public BookAppointmentController(IBookAppointmentServices appointmentServices, IRepository<int, Doctor> doctorRepository)
        {
            _appointmentServices = appointmentServices;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Booking()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return View(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> Book(int doctorId, DateTime appointmentDate, string appointmentTime)
        {
            // Hardcoding PatientId for now (replace with actual logged-in user logic)
            var patientId = 1; // Assuming user ID is 1 for development purposes

            if (DateTime.TryParse(appointmentDate.ToString("yyyy-MM-dd") + " " + appointmentTime, out DateTime appointmentStartDate))
            {
                // Check for conflict within 30 minutes of the desired appointment time
                var existingAppointments = await _appointmentServices.ShowAppointments(patientId);
                var hasConflict = existingAppointments.Any(a =>
                    (appointmentStartDate >= a.AppointmentStartDate && appointmentStartDate < a.AppointmentEndDate) ||
                    (appointmentStartDate.AddMinutes(30) > a.AppointmentStartDate && appointmentStartDate.AddMinutes(30) <= a.AppointmentEndDate)
                );

                if (hasConflict)
                {
                    ViewBag.Message = "You already have an appointment booked during this time. Please select another time.";
                    var doctors = await _doctorRepository.GetAllAsync();
                    return View("Booking", doctors);
                }

                // Proceed with booking the appointment
                var appointment = new Appointment
                {
                    DoctorId = doctorId,
                    PatientId = patientId,
                    AppointmentStartDate = appointmentStartDate
                };

                var success = await _appointmentServices.CreateAppointment(appointment);
                if (success != null)
                {
                    return RedirectToAction("MyAppointments");
                }

                ViewBag.Message = "Failed to book the appointment.";
            }
            else
            {
                ViewBag.Message = "Invalid date/time.";
            }

            var allDoctors = await _doctorRepository.GetAllAsync();
            return View("Booking", allDoctors);
        }

        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            var patientId = 1; // Use the actual logged-in user's ID
            var appointments = await _appointmentServices.ShowAppointments(patientId);
            return View(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var success = await _appointmentServices.DeleteAppointment(appointmentId);
            if (success != null)
            {
                return RedirectToAction("MyAppointments");
            }

            ViewBag.Message = "Failed to cancel the appointment.";
            return RedirectToAction("MyAppointments");
        }
    }
}
