using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegisterDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userServices.Register(createDTO);
                return Ok(new { 
                   
                    message="Your username is given below",
                    data=user
                });
                
            }
            else
            {
                throw new Exception("one or more validation errors");
            }

        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                var user = await _userServices.Login(loginRequest);
                return Ok(user);
            }
           
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<UserProfileDTO>> Get(string username)
        {
            try
            {
                var user = await _userServices.GetById(username);
                return Ok(user);
            }
            catch
            {
                throw new Exception("Cannot get profile");
            }
            
        }
      
        [HttpGet("GetAllUsers")]
         [Authorize(Roles ="Admin")]
        public async Task<ActionResult<User>> GetAll()
        {
            try
            {
                var users = await _userServices.GetAll();
                return Ok(users);
            }
            catch
            {
                throw new Exception("Cannot get all users");    
            }
            
        }

        [HttpPut("username")]
        public async Task<ActionResult<OperationStatusDTO>> UpdateUserPassword(string username,string password)
        {
            try
            {
                var status = await _userServices.UpdatePassword(username, password);
                return Ok(status);

            }
            catch
            {
                throw new Exception("Cannot update password");
            }


        }
    }
}

