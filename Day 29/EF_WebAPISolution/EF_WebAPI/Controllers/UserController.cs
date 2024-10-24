using EF_WebAPI.Interfaces;
using EF_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController( IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponseDTO>> Register(UserCreateDTO createDTO)
        {
            try
            {
                var user = await _userService.Register(createDTO);
                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not register user");
                return BadRequest(new ErrorResponseDTO
                {
                    ErrorMessage = e.Message,
                    ErrorNumber = 500
                });
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>>Login(LoginResponseDTO requestDTO)
        {
            try
            {
                var user = await _userService.Autheticate(requestDTO);
                return Ok(user.Username);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Invalid username or password");
                return Unauthorized(new ErrorResponseDTO
                {
                    ErrorMessage = e.Message,
                    ErrorNumber = 401
                });
            }
        }
      }
    }
