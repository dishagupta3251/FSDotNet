namespace PizzaApplication.Exceptions
{
    [Serializable]
    internal class NoSuchImageException : Exception
    {
        string mssg;
        public NoSuchImageException()
        {
            mssg = "There is no such image present";
        }

        
    }
}