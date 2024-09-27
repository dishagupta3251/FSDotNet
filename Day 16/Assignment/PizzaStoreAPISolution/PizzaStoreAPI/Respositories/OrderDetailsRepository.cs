using PizzaStoreAPI.Interfaces;
using PizzaStoreAPI.Models;

namespace PizzaStoreAPI.Respositories
{
    public class OrderDetailsRepository : IRepository<int, OrderDetails>
    {

        public Task<OrderDetails> Add(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> Get(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDetails>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> Update(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
