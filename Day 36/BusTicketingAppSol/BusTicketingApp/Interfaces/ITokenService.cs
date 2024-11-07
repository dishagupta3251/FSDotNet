using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
