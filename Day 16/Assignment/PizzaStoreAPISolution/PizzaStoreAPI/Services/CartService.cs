using System.Reflection.Metadata.Ecma335;
using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interface;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;
using PizzaStoreAPI.Models.DTOs;

namespace PizzaStoreAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<int, Cart> _cartRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Pizza> _pizzaRepository;
        public CartService(IRepository<int, Cart> cartRepository, IRepository<int, Customer> customerRepository, IRepository<int, Pizza> pizzaRepository)
        {
            _cartRepository = cartRepository;
            _customerRepository = customerRepository;
            _pizzaRepository = pizzaRepository;
        }

        public Task<IEnumerable<PizzaCartDTO>> AddPizzaToCart(PizzaCartDTO pizza, int customerId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCart(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PizzaCartDTO>> GetCart()
        {
            throw new NotImplementedException();
        }

        public Task<PizzaCartDTO> UpdateCart(PizzaCartDTO pizza)
        {
            throw new NotImplementedException();
        }
    }
}
