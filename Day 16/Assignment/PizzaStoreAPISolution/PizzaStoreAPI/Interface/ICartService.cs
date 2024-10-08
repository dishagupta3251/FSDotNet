using PizzaStoreAPI.Models.DTOs;

namespace PizzaStoreAPI.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<PizzaCartDTO>> AddPizzaToCart(PizzaCartDTO pizzaDTO, int customerId);
        Task<IEnumerable<PizzaCartDTO>> GetCart(int customerId);
        Task<PizzaCartDTO> UpdateCart(PizzaCartDTO pizzaDTO, int customerId);
    }
}
