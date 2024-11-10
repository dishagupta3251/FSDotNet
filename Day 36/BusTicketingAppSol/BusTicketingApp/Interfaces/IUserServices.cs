using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IUserServices
    {
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        public Task<string> Register(UserRegisterDTO user);

        public Task<IEnumerable<User>> GetAll();
        public Task<UserProfileDTO> GetById(string id);
        public Task<OperationStatusDTO> UpdatePassword(string username,string password);
        public Task<OperationStatusDTO> Delete(string key);
    }
}
