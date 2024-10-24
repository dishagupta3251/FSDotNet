using ReportClaim.Models.DTO;
using ReportClaim.Models;

namespace ReportClaim.Interfaces
{
    public interface IClaimService
    {
        public Task<Claim> CreateClaim(ClaimDTO claimDTO);
        public Task<IEnumerable<Claim>> GetAllClaims();
    }
}
