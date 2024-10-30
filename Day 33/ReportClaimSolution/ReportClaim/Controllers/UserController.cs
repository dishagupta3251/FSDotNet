using Microsoft.AspNetCore.Mvc;
using ReportClaim.Interfaces;
using ReportClaim.Models.DTO;

namespace ReportClaim.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        

        public UserController(IUserService userService)
        {
            _userService = userService;
           
        }
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponseDTO>> Register(UserCreateDTO createDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.Register(createDTO);
                    return Ok(user);
                }
                else
                {
                    return BadRequest(new ErrorReponseDTO
                    {
                        ErrorMessage = "one or more validation errors",
                        ErrorCode = 400
                    });
                }
            }
            catch (Exception e)
            {
                
                return BadRequest(new ErrorReponseDTO
                {
                    ErrorMessage = e.Message,
                    ErrorCode = 500
                });
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO requestDTO)
        {
            try
            {
                var user = await _userService.Authenticate(requestDTO);
                return Ok(user);
            }
            catch (Exception e)
            {
               
                return Unauthorized(new ErrorReponseDTO
                {
                    ErrorMessage = e.Message,
                    ErrorCode = 401
                });
            }

        }
    }
}
