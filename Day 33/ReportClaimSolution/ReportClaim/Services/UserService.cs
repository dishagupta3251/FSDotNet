using System.Security.Cryptography;
using System.Text;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Models.DTO;
using ReportClaim.Repositories;

namespace ReportClaim.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, string> _userRepository;
        private readonly ITokenService _tokenService;
        public UserService(IRepository<User,string> repository, ITokenService tokenService) {
            _userRepository = repository;
        
            _tokenService = tokenService;
        }
        public async Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginUser)
        {
            var user = await _userRepository.GetById(loginUser.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(user.HashKey);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.Password[i])
                {
                    throw new Exception("Invalid username or password");
                }
            }
            return new LoginResponseDTO()
            {
                Username = user.Username,
                Token = await _tokenService.GenerateToken(new UserTokenDTO()
                {
                    Username = user.Username,
                    Role = user.Role.ToString()
                })

            };
        }

        public async Task<LoginResponseDTO> Register(UserCreateDTO registerUser)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));
            User user = new User()
            {
                Username = registerUser.Username,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = registerUser.Role
            };
            try
            {
                var addesUser = await _userRepository.Create(user);
                LoginResponseDTO response = new LoginResponseDTO()
                {
                    Username = user.Username
                };
                return response;
            }
            catch (Exception e)
            {
               
                throw new Exception("Could not register user");
            }
        }
    }
}
