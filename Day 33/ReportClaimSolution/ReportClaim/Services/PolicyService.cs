using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Models.DTO;

namespace ReportClaim.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy,int> _policyRepository;
        private readonly IMapper _mapper;
        public PolicyService(IRepository<Policy,int> repository,IMapper mapper )
        {
            _policyRepository = repository;
            _mapper = mapper;
        }
        public async Task<Policy> CreatePolicy(PolicyDTO policyDTO)
        {
            try
            {
                var policy = _mapper.Map<Policy>(policyDTO);
                var result=await _policyRepository.Create(policy);
                return result;
            }
            catch 
            {
                throw new Exception("Cannot add policy");
            }
        }

        public async Task<IEnumerable<Policy>> GetAllPolicies()
        {
            try
            {
                var policies = await _policyRepository.GetAll();
                if (policies.Count() <= 0)
                    throw new Exception("Cannot provide all policies");
                return policies;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
