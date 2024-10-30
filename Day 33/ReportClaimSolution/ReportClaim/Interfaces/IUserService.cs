using ReportClaim.Models.DTO;

namespace ReportClaim.Interfaces
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Authenticate(LoginRequestDTO loginRequestDTO);
        public Task<LoginResponseDTO> Register(UserCreateDTO registerDTO);
    }
}
