using Login.Interfaces;
using Login.Models;
using Login.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Login.Controllers
{
    
    public class LoginController:Controller
    {
       private readonly ILoginService _loginService;
       public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
       
        [HttpGet]
        public IActionResult Sign_Up()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Sign_Up(User user)
        {
            
            var check= _loginService.Sign_Up(user);
            Console.WriteLine(check.Email);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Login() { 
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password) {
           var user=_loginService.Login(email,password);
            //Console.WriteLine(user.Email);
            if (user != null) {
                return RedirectToAction("Success");
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult ViewAllUsers()
        {
            var users = _loginService.Display();
            return View(users);
        }
    }
}
