namespace DoctorDetails
{
    internal class Program
    {
        public Details GetDetails(int Id)
        {
            Details details = new Details();
            details.TakeInput(Id);
            return details;
        }
         void ShowDetails(Details[] details)
        {
            for (int i = 0; i < details.Length; i++) {
                Console.WriteLine("ID: "+details[i].ID);
                Console.WriteLine( "Name: "+details[i].Name);
                Console.WriteLine("Email: "+details[i].Email);
                Console.WriteLine("Phone: "+details[i].Phone);
                Console.WriteLine("Address: "+details[i].Address);
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            Details[] details = new Details[3];
            int ID = 101;
            for (int i = 0; i < details.Length; i++) {
                Console.WriteLine("Your id is " + ID);
                details[i] = program.GetDetails(ID);
                ID += 1;
            }
            program.ShowDetails(details);
        }
    }
}
