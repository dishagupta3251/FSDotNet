using Login.Models;

namespace Login.Interfaces
{
    public interface IUser
    {
         User GetUser(string email,string password);
        public User AddUser(User user);

        public List<User> GetAll();
      
        User ForgotPassword(string email);

        void Delete(string email); 
    }
}
