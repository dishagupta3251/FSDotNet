using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorEF_Project.Repository
{
    public interface IRepository<T> where T: class
    {
        public T Insert();

        public T Get(string name);
        public List<T> GetAll();


    }
}
