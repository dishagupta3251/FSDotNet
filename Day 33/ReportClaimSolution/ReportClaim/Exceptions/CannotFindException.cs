using System.Runtime.Serialization;

namespace ReportClaim.Exceptions
{
    [Serializable]
    public class CannotFindException : Exception
    {
        string mssg;
        public CannotFindException(string str)
        {
            mssg = $"Cannot find {str}";
        }

        public override string Message => mssg;
    }
}