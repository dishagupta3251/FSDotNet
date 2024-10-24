using System.Runtime.Serialization;

namespace ReportClaim.Exceptions
{
    [Serializable]
    public class CannotCreateException : Exception
    {
        string mssg;
        public CannotCreateException(string mssg)
        {
            mssg = $"Cannot create {mssg}";
        }
        public override string Message => mssg;

    }
}