using PizzaApplication.Exceptions;
using PizzaApplication.Interfaces;
using PizzaApplication.Models;

namespace PizzaApplication.Repository
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        List<Pizza> pizzas = new List<Pizza>()
        {
            new Pizza(){Id = 1, Name = "Margherita", Description = "Cheese and Tomato", Price = 5.99f, PictureUrl = "margherita.jpg"},
            new Pizza(){Id = 2, Name = "Pepperoni", Description = "Pepperoni and Cheese", Price = 7.99f, PictureUrl = "pepperoni.jpg"}
        };

        public void Add(Pizza pizza)
        {
           pizzas.Add(pizza);
        }

        public Pizza Delete(int id)
        {
           var pizza = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                pizzas.Remove(pizza);
            }
            else { throw new NoSuchPIzzaFound(); } return pizza;
        }

        public Pizza Get(int id)
        {
            var pizza = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza == null)
                throw new NoSuchPIzzaFound();
            return pizza;

        }

        public List<Pizza> GetAll()
        {
            if (pizzas.Count == 0)
                throw new NoPizzaException();
            return pizzas;

        }

        public Pizza Update(Pizza pizza)
        {
            var myPizza = Get(pizza.Id);
            if (myPizza == null)
                throw new NoSuchPIzzaFound();
            myPizza.Name = pizza.Name;
            myPizza.Description = pizza.Description;
            myPizza.Price = pizza.Price;
            myPizza.PictureUrl = pizza.PictureUrl;
            return myPizza;

        }
    }
}
