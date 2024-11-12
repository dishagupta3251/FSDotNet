using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Services
{
    public class UserService : IUserServices
    {
        private readonly IRepository<User, string> _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<User, string> repository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = repository;
            _tokenService = tokenService;
            _mapper = mapper;

        }
        public async Task<OperationStatusDTO> Delete(string key)
        {
            var _user = await _userRepository.Delete(key);
            string status = "Delete Failed";
            if (_user != null)
            {
                status = "Delete Successful";
            }
            var result = new OperationStatusDTO()
            {
                Username = _user.Username,
                Status = status
            };
            return result;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                if (users.Count() == 0) throw new Exception("Cannot fetch all users");

                return users;
            }
            catch
            {
                throw new Exception("Error");
            }

        }

        public async Task<UserProfileDTO> GetById(string username)
        {
            var _user = await _userRepository.Get(username);
            var user = _mapper.Map<UserProfileDTO>(_user);
            return user;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {

            var user = await _userRepository.Get(loginRequestDTO.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(user.PasswordHash);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequestDTO.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.Password[i])
                {
                    throw new Exception("Invalid  password");
                }
            }
            return new LoginResponseDTO()
            {
                Username = user.Username,
                Token = await _tokenService.GenerateToken(new UserTokenDTO()
                {
                    Username = user.Username,
                    Role = user.Role.ToString()
                }),
                UserId=user.UserId

            };
        }

        public async Task<string> Register(UserRegisterDTO user)
        {
            try
            {
                HMACSHA256 hmac = new HMACSHA256();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                User _user = new User()
                {
                    FirstName = user.FirstName,
                    Email = user.Email,
                    ContactNumber = user.ContactNumber,
                    Username = user.FirstName +     user.ContactNumber.Substring(7),
                    Password = passwordHash,
                    PasswordHash = hmac.Key,
                    Role = user.Role
                };

                var addesUser = await _userRepository.Add(_user);
              
                return _user.Username;
            }
            catch (Exception e)
            {

                throw new Exception("Could not register user");
            }
        }


        public async Task<OperationStatusDTO> UpdatePassword(string username,string password)
        {
            var user = await _userRepository.Get(username);
            using var hmac = new HMACSHA256();
            user.PasswordHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var result = await _userRepository.Update(user,username);

            return new OperationStatusDTO
            {
                Username = result.Username,
                Status = "Update Successful"
            };
        }
    }


}
