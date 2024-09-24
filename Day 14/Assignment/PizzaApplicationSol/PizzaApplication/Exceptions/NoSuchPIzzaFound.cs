using Microsoft.VisualBasic;

namespace PizzaApplication.Exceptions
{
    public class NoSuchPIzzaFound : Exception
    {
        string msg;
        public NoSuchPIzzaFound() {
            msg = "No pizza found with this id";
        }
        public override string Message => msg;
    }
}
