using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Respositories
{
    public class OrderRepository : IRepository<int, Order>
    {
        List<Order> _orders=new List<Order>();
        public async Task<Order> Add(Order entity)
        {
            _orders.Add(entity);
            return entity;
        }

        public async Task<Order> Delete(int key)
        {
            var order= await Get(key);
            _orders.Remove(order);
            return order;
        }

        public async Task<Order> Get(int key)
        {
            var order=_orders.FirstOrDefault(o=>o.OrderNumber == key);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return _orders;
        }

        public Task<Order> Update(Order entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
