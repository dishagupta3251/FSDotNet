using System.Runtime.Serialization;

namespace BusTicketingApp.Exceptions
{

  public class NotFoundException : Exception
    {
        string mssg;
        public NotFoundException(string str)
        {
            mssg = "Cannot find "+str;
        }

        public override string Message => mssg;
    }
}