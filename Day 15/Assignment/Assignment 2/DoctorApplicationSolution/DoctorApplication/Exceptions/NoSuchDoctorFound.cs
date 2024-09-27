namespace DoctorApplication.Exceptions
{
    [Serializable]
    internal class NoSuchDoctorFound : Exception
    {
        string message;
        public NoSuchDoctorFound()
        {
            message = "No doctor found with this id";
        }

        public override string Message => message;
    }
}