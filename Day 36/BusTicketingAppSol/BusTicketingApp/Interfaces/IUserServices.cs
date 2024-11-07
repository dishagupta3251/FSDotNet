using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IUserServices
    {
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        public Task<LoginResponseDTO> Register(UserRegisterDTO user);

        public Task<IEnumerable<User>> GetAll();
        public Task<UserProfileDTO> GetById(string id);
        public Task<OperationStatusDTO> Update(UserRegisterDTO user,string key);
        public Task<OperationStatusDTO> Delete(string key);
    }
}
