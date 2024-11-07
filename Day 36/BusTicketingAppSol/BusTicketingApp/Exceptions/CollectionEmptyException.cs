using System.Runtime.Serialization;

namespace BusTicketingApp.Exceptions
{
  
  public class CollectionEmptyException : Exception
    {
        string mssg;
        public CollectionEmptyException(string str)
        {
            mssg = str + " is empty";
        }
        public override string Message => mssg;

    }
}