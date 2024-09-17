namespace Calculator
{
    internal class Program
    {
        IValidateAge validateAge;
        public Program() {
            validateAge = new Validate();
            validateAge.ValidateAge();
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
