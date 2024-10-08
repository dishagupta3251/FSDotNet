namespace PizzaStoreAPI.Models.DTOs
{
    public class PizzaCartDTO : IEquatable<PizzaCartDTO>
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Toppings { get; set; }

        public bool Equals(PizzaCartDTO? other)
        {
            return (this ?? new PizzaCartDTO()).PizzaId == (other ?? new PizzaCartDTO()).PizzaId;
        }
    }
}