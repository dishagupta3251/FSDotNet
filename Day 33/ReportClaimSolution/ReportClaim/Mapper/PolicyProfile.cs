using AutoMapper;
using ReportClaim.Models;
using ReportClaim.Models.DTO;

namespace ReportClaim.Mapper
{
    public class PolicyProfile:Profile
    {
        public PolicyProfile()
        {
            CreateMap<PolicyDTO, Policy>();
            CreateMap<Policy, PolicyDTO>();

        }
    }
}
