namespace ReportClaim.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public IEnumerable<Report> Reports { get; set; }

        public Claim()
        {
            Reports=new List<Report>();
        }
    }
}
