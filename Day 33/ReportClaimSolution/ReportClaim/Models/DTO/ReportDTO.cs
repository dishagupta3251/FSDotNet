namespace ReportClaim.Models.DTO
{
    public class ReportDTO
    {
        public int PolicyId { get; set; }
        public int ClaimId { get; set; }
        public DateTime IncidentDate { get; set; }
        public string ClaimaintName { get; set; } = string.Empty;
        public string ClaimaintPhone { get; set; } = string.Empty;
        public string ClaimaintEmail { get; set; } = string.Empty;

        public IFormFile? PhotoId { get; set; }  
        public IFormFile? SettlementForm { get; set; }
        public IFormFile? DeathCertificate { get; set; }
        public IFormFile? PolicyCertificate { get; set; }
        public IFormFile? AddressProof { get; set; }
        public IFormFile? CancelledCheck { get; set; }
        public IFormFile? Others { get; set; }
    }
}
