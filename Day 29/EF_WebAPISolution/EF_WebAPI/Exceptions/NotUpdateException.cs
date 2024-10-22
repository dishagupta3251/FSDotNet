namespace UnitTest1
{
    public class NotUpdateException:Exception
    {
        string str;
        public NotUpdateException()
        {
            str = "Not Update";
        }
    }
}