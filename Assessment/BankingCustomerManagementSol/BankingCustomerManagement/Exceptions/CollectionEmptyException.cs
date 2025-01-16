using System.Runtime.Serialization;

namespace BankingCustomerManagement.Exceptions
{
    [Serializable]
    public class CollectionEmptyException : Exception
    {
        string mssg;
        public CollectionEmptyException(string str)
        {
            mssg = "CollectionEmptyException: "+ str;
        }
        public override string Message => mssg;


    }
}