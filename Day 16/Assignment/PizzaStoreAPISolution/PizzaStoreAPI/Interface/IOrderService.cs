using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Interface
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(int customerId);
        Task<IEnumerable<OrderDetailsDTO>> GetOrderDetails(int orderNumber);
    }
}
