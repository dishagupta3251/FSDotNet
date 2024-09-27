namespace PizzaStoreAPI.Exceptions
{
    [Serializable]
    internal class ToppingNotFoundException : Exception
    {
        string mssg = "";
        public ToppingNotFoundException()
        {
            mssg = "Topping with this id is not available";
        }

        public override string Message => mssg;
    }
}