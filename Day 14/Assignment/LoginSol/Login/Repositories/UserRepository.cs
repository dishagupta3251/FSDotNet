using Login.Interfaces;
using Login.Models;

namespace Login.Repository
{
    public class UserRepository : IUser
    {
        private readonly List<User> _users=new List<User>()
        {
            new User(){Email="abc@gmail.com",Password="abc@123"},
            new User(){Email="xyz@gmail.com",Password="xyz@123"}
        };

        public User AddUser(User user)
        {
           _users.Add(user);
            return user;
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public void Delete(string email)
        {
            var user=_users.FirstOrDefault(x => x.Email == email);
            if (user != null) {_users.Remove(user); }
        }

        public User ForgotPassword(string email)
        {
            var user = _users.FirstOrDefault(x => x.Email == email);
            return user;
        }

        public User GetUser(string email, string password)
        {
            return _users.FirstOrDefault(u=>u.Email==email&& u.Password==password);
        }
    }
}

