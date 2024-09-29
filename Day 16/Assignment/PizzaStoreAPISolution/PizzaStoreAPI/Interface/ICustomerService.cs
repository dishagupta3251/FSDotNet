using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Interface
{
    public interface ICustomerService
    {
        public Task<IEnumerable<Pizza>> GetAllPizzas();
        public Task<IEnumerable<Toppings>> GetAllToppings();
    }
}
