using System.Runtime.Serialization;

namespace BusTicketingApp.Exceptions
{
    
   public class CouldNotAddException : Exception
    {
        string mssg;
        public CouldNotAddException(string str)
        {
            mssg = "Could not add "+str;
        }
        public override string Message => mssg;

    }
}