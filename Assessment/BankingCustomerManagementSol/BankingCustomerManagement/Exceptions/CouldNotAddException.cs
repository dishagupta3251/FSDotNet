using System.Runtime.Serialization;

namespace BankingCustomerManagement.Exceptions
{
    [Serializable]
    public class CouldNotAddException : Exception
    {
        string mssg;
        public CouldNotAddException(string str)
        {
            mssg = "CouldNotAddException: "+str;
        }
        public override string Message => mssg;
    }
}