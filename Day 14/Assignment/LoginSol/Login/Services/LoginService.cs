using Login.Interfaces;
using Login.Models;

namespace Login.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUser _user;
        public LoginService(IUser repository)
        {
            _user = repository;
        }
        public void Delete(string email)
        {
           _user.Delete(email);
        }

        public List<User> Display()
        {
            return _user.GetAll();
        }

        public User Login(string email, string password)
        {
           return _user.GetUser(email, password);
        }

        public User Sign_Up(User user)
        {
            var usern=_user.AddUser(user);
            return usern;
        }

        
    }
}
