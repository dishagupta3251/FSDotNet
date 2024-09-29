using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Respositories
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        static List<Pizza> pizzaList = new List<Pizza>()
        {
            new Pizza(){Id=101,Price=235,Name="Pepproni",Description="dewnfuifb",Image="",Quantity=10},
            new Pizza(){Id=102,Price=435,Name="Margrita",Description="hgvgvhg",Image="",Quantity=20}

        };  
        public  async Task<Pizza> Add(Pizza entity)
        {
            pizzaList.Add(entity);
            return entity;
        }

        public async Task<Pizza> Delete(int key)
        {
            var pizza= await Get(key);
            pizzaList.Remove(pizza);
            return pizza;
        }

        public async Task<Pizza> Get(int key)
        {
            var pizza = pizzaList.FirstOrDefault(p=>p.Id== key);
            if(pizza==null) throw new NoEntityFoundException();
            return pizza;
        }

        public async Task<IEnumerable<Pizza>> GetAll()
        {
            return pizzaList;
        }

        public async Task<Pizza> Update(Pizza entity)
        {
            var oldPizza =await  Get(entity.Id);
            oldPizza.Price = entity.Price;
            oldPizza.Quantity = entity.Quantity;
            oldPizza.Description = entity.Description;
            return entity;

        }
    }
}
