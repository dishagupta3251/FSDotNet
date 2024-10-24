using AutoMapper;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Models.DTO;
using ReportClaim.Repositories;

namespace ReportClaim.Services
{
    public class ClaimService : IClaimService
    {

        private readonly IRepository<Claim, int> _claimRepository;
        private readonly IMapper _mapper;
        public ClaimService(IRepository<Claim, int> repository, IMapper mapper)
        {
            _claimRepository = repository;
            _mapper = mapper;
        }
        public async Task<Claim> CreateClaim(ClaimDTO claimDTO)
        {
            try
            {
                Claim claim = _mapper.Map<Claim>(claimDTO);
                
                var result=await _claimRepository.Create(claim);
                return result;
            }
            catch
            {
                throw new Exception("Cannot add claim");
            }
        }

        public async Task<IEnumerable<Claim>> GetAllClaims()
        {
            try
            {
                var claims = await _claimRepository.GetAll();
                
                if (claims.Count() <= 0)
                    throw new Exception("Cannot provide all policies");
                return claims;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
