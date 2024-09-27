namespace PizzaStoreAPI.Models
{
    public class Toppings:IEquatable<Toppings>
    {
        public int Id { get; set; } 
        public string Pizza_Toppings { get; set; }

        public bool Equals(Toppings? other)
        {
            return (this ?? new Toppings()).Id == (other ?? new Toppings()).Id;
        }
    }
}
