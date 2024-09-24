using PizzaApplication.Models;

namespace PizzaApplication.Interfaces
{
    public interface IRepository<K,T> where T : class
    {
        void Add(T item);
        T Get(K key);
        List<T> GetAll();
        T Update(T item);
        T Delete(K key);

    }
}
