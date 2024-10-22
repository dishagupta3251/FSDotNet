using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Interfaces
{
    public interface IUserService
    {
        public Task<User> Add(UserDTO userDTO);
        public Task<IEnumerable<User>> GetAll();

    }
}
