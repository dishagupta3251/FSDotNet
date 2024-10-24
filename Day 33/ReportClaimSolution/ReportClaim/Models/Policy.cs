namespace ReportClaim.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public IEnumerable<Report> Reports { get; set; }
        public Policy()
        {
            Reports=new List<Report>();
        }
    }
}
