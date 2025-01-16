using System.Runtime.Serialization;

namespace BankingCustomerManagement.Exceptions
{
    [Serializable]
    public class CouldNotDeleteException : Exception
    {
        string mssg;
        public CouldNotDeleteException(string str)
        {
            mssg = "CouldNotDeleteException"+str;
        }
        public override string Message => mssg;
    }
}