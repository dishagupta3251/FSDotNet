namespace WebApplication1.Interfaces
{
    public interface IRepository<K,T > where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(K key);
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(K key);

    }
}
