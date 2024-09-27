using DoctorApplication.Exceptions;
using DoctorApplication.Interfaces;
using DoctorApplication.Models;
using DoctorApplication.Respositories;

namespace DoctorApplication.Services
{
    public class LoginUserServices : ILoginUserService
    {
        private readonly IRepository<string,User> _userRepository;
        public LoginUserServices(IRepository<string, User> userRepository) {
            _userRepository = userRepository;
        }

        public async Task<User> UserLogin(string username, string password)
        {
            var user = await _userRepository.GetAsync(username);

            if (user == null)
            {
                throw new UserNotFoundException();
               
            }
            else {
                if (user.Password == password)
                {
                    return user;
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
           

            return null;
        }
    }
    }

