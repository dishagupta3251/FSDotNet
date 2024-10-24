using System.Runtime.Serialization;

namespace ReportClaim.Exceptions
{
    [Serializable]
    public class CannotUpdateException : Exception
    {
        string mssg;
        public CannotUpdateException(string str)
        {
            mssg = "CannotUpdate"+str;
        }

        override public string  Message=>mssg;
    }
}