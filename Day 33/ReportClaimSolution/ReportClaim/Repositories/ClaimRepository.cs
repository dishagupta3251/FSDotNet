
using Microsoft.EntityFrameworkCore;
using ReportClaim.Contexts;
using ReportClaim.Exceptions;
using ReportClaim.Interfaces;
using ReportClaim.Models;

namespace ReportClaim.Repositories
{
    public class ClaimRepository : IRepository<Claim, int>
    {
        private readonly ReportClaimContext _context;
        public ClaimRepository(ReportClaimContext reportClaimContext)
        {
            _context = reportClaimContext;
        }
        public async Task<Claim> Create(Claim entity)
        {
            try
            {
                _context.Claims.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CannotCreateException("Claim");
            }
        }

        public async Task<Claim> Delete(Claim entity)
        {
            try
            {
                _context.Claims.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch {
                throw new Exception("Claim does not exsists");
            }
           
        }

        public async Task<IEnumerable<Claim>> GetAll()
        {
            var claims = await _context.Claims.ToListAsync();
            return claims;
        }

        public async Task<Claim> GetById(int id)
        {
            var claim = await _context.Claims.FirstOrDefaultAsync(c => c.ClaimId == id);
            if (claim == null)
            {
                throw new CannotFindException("Claim");
            }
            return claim;

        }

        public async Task<Claim> Update(Claim entity)
        {
            try
            {
                var oldClaim = await GetById(entity.ClaimId);
                oldClaim.ClaimType = entity.ClaimType;
                return oldClaim;

            }
            catch 
            {
                throw new CannotUpdateException("Claim");
            }
        }
    }
}
