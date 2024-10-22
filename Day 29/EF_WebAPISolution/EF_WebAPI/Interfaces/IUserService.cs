using EF_WebAPI.Models.DTO;

namespace EF_WebAPI.Interfaces
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Autheticate(LoginResponseDTO loginUser);
        public Task<LoginResponseDTO> Register(UserCreateDTO registerUser);

    }
}
