namespace DoctorApplication.Exceptions
{
    [Serializable]
    internal class NoSuchAppointmentException : Exception
    {
        string mssg;
        public NoSuchAppointmentException()
        {
            mssg = "No appointment with this id found";
        }
        public override string Message => mssg;

    }
}