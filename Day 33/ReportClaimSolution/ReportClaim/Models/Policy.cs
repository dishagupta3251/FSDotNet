using System.ComponentModel.DataAnnotations;
using ReportClaim.Validation;

namespace ReportClaim.Models
{
    public class Policy
    {
        public int Id { get; set; }
        [PolicyNumberValidator]
        public string PolicyNumber { get; set; } = string.Empty;
        public IEnumerable<Report> Reports { get; set; }
        public Policy()
        {
            Reports=new List<Report>();
        }
    }
}
