using System.Runtime.Serialization;

namespace ReportClaim.Exceptions
{
    class CollectionEmptyException : Exception
    {
        string mssg = string.Empty;
        public CollectionEmptyException(string str)
        {
            mssg = "Collection is empty"+str;
        }
        public override string Message => mssg;
    }
  

     
}