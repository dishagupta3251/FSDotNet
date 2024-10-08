namespace PizzaStoreAPI.Models.DTOs
{
    
        public class OrderDetailsDTO
        {
            public int OrderNumber { get; set; }
            public int PizzaId { get; set; }
            public string PizzaName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

    }
