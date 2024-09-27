
using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Respositories
{
    public class ToppingsRepository : IRepository<int, Toppings>
    {
        List<Toppings> _toppings=new List<Toppings>()
        {
            new Toppings(){Pizza_Toppings="Onion"} ,
            new Toppings(){Pizza_Toppings="Tomato" } ,
            new Toppings(){Pizza_Toppings="Olives" } 
        };
        public async Task<Toppings> Add(Toppings entity)
        {

            _toppings.Add(entity);
            return entity;
        }

        public async Task<Toppings> Delete(int key)
        {
            var topping = await Get(key);
            if (topping==null)
            {
                throw new ToppingNotFoundException();
            }
            _toppings.Remove(topping);
            return topping;
        }

        public async Task<Toppings> Get(int key)
        {
            var topping = _toppings.FirstOrDefault(t => t.Id == key);
            return topping;
        }

        public async Task<IEnumerable<Toppings>> GetAll()
        {
            return _toppings;
        }


        public async Task<Toppings> Update(Toppings entity)
        {
            var oldTopping =await Get(entity.Id);
            oldTopping.Pizza_Toppings= entity.Pizza_Toppings;
            return entity;
        }
    }
}
