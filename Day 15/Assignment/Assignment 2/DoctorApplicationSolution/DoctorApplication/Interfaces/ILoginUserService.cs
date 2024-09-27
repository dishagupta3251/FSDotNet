using DoctorApplication.Models;

namespace DoctorApplication.Interfaces
{
    public interface ILoginUserService
    {
        public Task<User> UserLogin(string username, string password);

    }
}
