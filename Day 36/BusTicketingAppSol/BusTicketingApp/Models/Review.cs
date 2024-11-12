namespace BusTicketingApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public string Reviews {  get; set; }=string.Empty;

        public BusOperator Operator { get; set; }
    }
}
