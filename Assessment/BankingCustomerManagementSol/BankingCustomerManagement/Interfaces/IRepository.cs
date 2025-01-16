namespace BankingCustomerManagement.Interfaces
{
    public interface IRepository<T,K> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity, K key);
        public Task<T> Delete(K key);
        public Task<T> Get(K key);
        public Task<IEnumerable<T>> GetAll();
    }
}
