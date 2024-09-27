namespace DoctorApplication.Exceptions
{
    [Serializable]
    internal class UserNotFoundException : Exception
    {
        string mssg;
        public UserNotFoundException()
        {
            mssg = "Username not found";
        }
        public override string Message => mssg;
    }
}