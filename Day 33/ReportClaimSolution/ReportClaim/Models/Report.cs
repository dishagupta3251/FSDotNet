using System.ComponentModel.DataAnnotations.Schema;

namespace ReportClaim.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string ClaimaintName { get; set; } = string.Empty;
        public string ClaimaintPhone { get; set; } = string.Empty;
        public string ClaimaintEmail { get; set; } = string.Empty;
        public string SettlementForm { get; set; } = string.Empty;
        public string DeathCertificate { get; set; } = string.Empty;
        public string PolicyCertificate { get; set; } = string.Empty;
        public string PhotoId { get; set; } = string.Empty;
        public string AddressProof { get; set; } = string.Empty;
        public string CancelledCheck { get; set; } = string.Empty;
        public string Others { get; set; } = string.Empty;

        // Foreign Keys
        public int ClaimId { get; set; }    // Foreign key for Claim
        public Claim Claim { get; set; }    // Navigation property

        public int PolicyId { get; set; }   // Foreign key for Policy
        public Policy Policy { get; set; }  // Navigation property

        public Report()
        {
            Policy = new Policy();
            Claim = new Claim();
        }


    }
}
