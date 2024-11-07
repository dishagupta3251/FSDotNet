using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponseDTO>> Register(UserRegisterDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userServices.Register(createDTO);
                return Ok(user);
            }
            else
            {
                throw new Exception("one or more validation errors");
            }

        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequest)
        {

            var user = await _userServices.Login(loginRequest);
            return Ok(user);

        }
        [HttpGet("GetById")]
        public async Task<ActionResult<UserProfileDTO>> Get(string username)
        {
            var user = await _userServices.GetById(username);
            return Ok(user);
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<User>> GetAll()
        {
            var users=await _userServices.GetAll();
            return Ok(users);
        }
        [HttpPut("username")]
        public async Task<ActionResult<OperationStatusDTO>> Update(UserRegisterDTO userRegister,string username)
        {
               var status = await _userServices.Update(userRegister, username);
               return Ok(status);

        }
    }
}

