using ReportClaim.Models;
using ReportClaim.Models.DTO;

namespace ReportClaim.Interfaces
{
    public interface IPolicyService
    {
        public Task<Policy> CreatePolicy(PolicyDTO policyDTO);
        public Task<IEnumerable<Policy>> GetAllPolicies();
    }
}
