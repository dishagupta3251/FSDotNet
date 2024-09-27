using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Respositories
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        List<Pizza> pizzaList = new List<Pizza>()
        {
            new Pizza(){Id=101,Price=235,Name="Pepproni",Description="dewnfuifb",Image="",Quantity=10},
            new Pizza(){Id=102,Price=435,Name="Margrita",Description="hgvgvhg",Image="",Quantity=20}

        };  
        public  async Task<Pizza> Add(Pizza entity)
        {
            pizzaList.Add(entity);
            return entity;
        }

        public Task<Pizza> Delete(int key)
        {
            
        }

        public async Task<Pizza> Get(int key)
        {
            var pizza = pizzaList.FirstOrDefault(p=>p.Id== key);
            return pizza;
        }

        public Task<IEnumerable<Pizza>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Pizza> Update(Pizza entity)
        {
            throw new NotImplementedException();
        }
    }
}
