using PizzaApplication.Exceptions;
using PizzaApplication.Interfaces;
using PizzaApplication.Models;

namespace PizzaApplication.Repository
{
    public class PizzaImageRepository : IRepository<int, PizzaImages>
    {
        List<PizzaImages> images=new List<PizzaImages>()
        {
            new PizzaImages(){Id=1,Images=new List<string>(){"margretia.jpg","pepperoni.jpg"} },
            new PizzaImages(){Id=2,Images=new List<string>(){ "margretia.jpg", "pepperoni.jpg" } }
            };
        public void Add(PizzaImages item)
        {
           images.Add(item);
        }

        public PizzaImages Delete(int key)
        {
            var image=images.FirstOrDefault(x => x.Id == key);
            if (image != null)
            {
                images.Remove(image);
                return image;
            }
            else { throw new NoSuchImageException(); }
            
        }

        public PizzaImages Get(int key)
        {
            var image = images.FirstOrDefault(p => p.Id == key);
            return image;
        }

        public List<PizzaImages> GetAll()
        {
            return images;
        }

        public PizzaImages Update(PizzaImages image)
        {
            var oldImage=Get(image.Id);
            oldImage.Images=image.Images;
            return image;


        }

       
    }
}
