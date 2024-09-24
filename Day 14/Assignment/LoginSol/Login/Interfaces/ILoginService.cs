using Login.Models;

namespace Login.Interfaces
{
    public interface ILoginService
    {
        
        User Login(string email, string password);
        User Sign_Up(User user);

        List<User> Display();
        void Delete(string email);



    }
}
