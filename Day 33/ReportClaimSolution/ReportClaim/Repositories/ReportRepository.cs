using Microsoft.EntityFrameworkCore;
using ReportClaim.Contexts;
using ReportClaim.Exceptions;
using ReportClaim.Interfaces;
using ReportClaim.Models;

namespace ReportClaim.Repositories
{
    public class ReportRepository : IRepository<Report, int>
    {
        private readonly ReportClaimContext reportClaim;
        public ReportRepository(ReportClaimContext reportClaimContext)
        {
            reportClaim = reportClaimContext;
        }
        public async Task<Report> Create(Report entity)
        {
            try
            {
                reportClaim.Reports.Add(entity);
                await reportClaim.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CannotCreateException("Report");
            }
        }

        public async Task<Report> Delete(Report entity)
        {
            try
            {
                reportClaim.Reports.Remove(entity);
                await reportClaim.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new Exception("Cannot delete");
            }
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            var reports = await reportClaim.Reports.ToListAsync();
            return reports;
        }

        public async Task<Report> GetById(int id)
        {
              var report = await reportClaim.Reports.FirstOrDefaultAsync(policy => policy.Id == id);
                if (report == null)
                {
                    throw new CannotFindException("Report");
                }
                return report;
            
         }
        

        public async Task<Report> Update(Report entity)
        {
            try
            {
                var oldReport = await GetById(entity.Id);
                oldReport.ClaimaintName = entity.ClaimaintName;
                oldReport.ClaimaintPhone = entity.ClaimaintPhone;
                oldReport.CancelledCheck = entity.CancelledCheck;
                oldReport.AddressProof = entity.AddressProof;
                oldReport.DeathCertificate = entity.DeathCertificate;
                oldReport.PolicyCertificate = entity.PolicyCertificate;
                oldReport.SettlementForm = entity.SettlementForm;
                return oldReport;

            }
            catch
            {
                throw new CannotUpdateException("Policy");
            }
        }
    }
}
