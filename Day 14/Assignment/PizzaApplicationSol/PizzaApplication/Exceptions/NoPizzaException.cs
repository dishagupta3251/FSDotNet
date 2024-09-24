namespace PizzaApplication.Exceptions
{
    public class NoPizzaException:Exception
    {
        string message;
        public NoPizzaException() {
            message = "No pizza";
        }

        public override string Message=>message;
    }
}
