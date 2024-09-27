using DoctorApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoctorApplication.Controllers
{
    public class LoginController:Controller
    {
        ILoginUserService _loginUserService;
        public LoginController(ILoginUserService loginUserService)
        {
            _loginUserService = loginUserService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password) { 
           var user= _loginUserService.UserLogin(username, password);
            if (user == null) {
                return View();
            }
            return RedirectToAction("Booking","BookAppointment");
        }
    }
}
