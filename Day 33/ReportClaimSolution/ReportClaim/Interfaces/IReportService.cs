using ReportClaim.Models;
using ReportClaim.Models.DTO;

namespace ReportClaim.Interfaces
{
    public interface IReportService
    {
        public Task<Report> CreateReport(ReportDTO reportDTO);
        public Task<IEnumerable<Report>> GetAllReports();
    }
}
