using System.Runtime.Serialization;

namespace BankingCustomerManagement.Exceptions
{
    [Serializable]
    public class CouldNotUpdateException : Exception
    {
        string mssg;
        public CouldNotUpdateException(string str)
        {
            mssg = " CouldNotUpdateException: "+str;
        }
        public override string Message => mssg;
    }
}