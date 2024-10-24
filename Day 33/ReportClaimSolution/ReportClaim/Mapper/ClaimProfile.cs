
using AutoMapper;
using ReportClaim.Models;
using ReportClaim.Models.DTO;

namespace ReportClaim.Mapper
{
    public class ClaimProfile:Profile
    {
        public ClaimProfile()
        {
            CreateMap<ClaimDTO, Claim>();
            CreateMap<Claim,ClaimDTO>();
        }
    }
}
