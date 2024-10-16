namespace EF_WebAPI.Interfaces
{
    public interface IRepository<K, T> where T: class 
    {
        public Task<T> Add(T entity);
        public Task<T> Delete(K key);
        public Task<T> Get(K key);
        public Task<IEnumerable<T>> GetAll();

        public Task<T> Update(K key, T entity);
    }
}
