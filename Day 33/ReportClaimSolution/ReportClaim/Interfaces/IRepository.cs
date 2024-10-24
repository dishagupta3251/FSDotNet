namespace ReportClaim.Interfaces
{
    public interface IRepository<T, K> where T : class
    {
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(T entity);
        public Task<T> GetById(K id);
        public Task<IEnumerable<T>> GetAll();


    }
}
