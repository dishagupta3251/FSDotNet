using System.Runtime.Serialization;

namespace BankingCustomerManagement.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        string mssg;
        public NotFoundException(string str)
        {
            mssg = "NotFoundException: "+str;
        }
        public override string Message => mssg;
    }
}