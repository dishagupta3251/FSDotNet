using ReportClaim.Models.DTO;

namespace ReportClaim.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
