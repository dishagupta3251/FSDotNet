using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ReportClaim.Contexts;
using ReportClaim.Exceptions;
using ReportClaim.Interfaces;
using ReportClaim.Models;

namespace ReportClaim.Repositories
{
    public class PolicyRepository : IRepository<Policy, int>
    {
        private readonly ReportClaimContext _context;
        public PolicyRepository(ReportClaimContext reportClaimContext)
        {
            _context = reportClaimContext;
        }
        public async Task<Policy> Create(Policy entity)
        {
            try
            {
                _context.Policies.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch 
            {
                throw new CannotCreateException("Policy");
            }
              
        }

        public async Task<Policy> Delete(Policy entity)
        {
            try
            {
                _context.Policies.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch {
                throw new Exception("Cannot delete");
            }

        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            var policies= await _context.Policies.ToListAsync();
            return policies;
        }

        public async Task<Policy> GetById(int id)
        {
              var policy = await _context.Policies.FirstOrDefaultAsync(policy => policy.Id == id);
            if (policy == null)
            {
                throw new CannotFindException("Policy");
            }
            return policy;

        }

        public async Task<Policy> Update(Policy entity)
        {
            try
            {
                var oldPolicy=await GetById(entity.Id);
                oldPolicy.PolicyNumber = entity.PolicyNumber;
                return oldPolicy;

            }
            catch  {
                throw new CannotUpdateException("Policy");
            }
        }
    }
}
