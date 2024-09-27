using Microsoft.AspNetCore.Mvc;

namespace DoctorApplication.Controllers
{
    public class BookAppointmentController:Controller
    {
        public BookAppointmentController() { }

        public  IActionResult Booking()
        {
            return View();
        }
    }
}
