using PizzaStoreAPI.Interface;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<int, Pizza> _pizzaRepository;
        private readonly IRepository<int, Toppings> _toppingRepository;
        public CustomerService(IRepository<int,Pizza> repository,IRepository<int,Toppings> repository1) {
            _pizzaRepository = repository;
            _toppingRepository = repository1;
        }

        public async Task<IEnumerable<Pizza>> GetAllPizzas()
        {
             
           return (await _pizzaRepository.GetAll()).ToList().OrderBy(p => p.Id); ;
        }

        public async Task<IEnumerable<Toppings>> GetAllToppings()
        {
            return (await _toppingRepository.GetAll()).ToList().OrderBy(p => p.Id); ;
        }
    }
}
