namespace College
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; }= string.Empty;
        public string Phone { get; set; }
        = string.Empty;

        public DateOnly DateofBirth { get; set; }   
        static void Main(string[] args)
        {
            Student student = new Student();
            student.Id = 101;
            student.Name = "Ramu";
            student.DateofBirth = new DateOnly(2001,07,12);
            student.Email = "ramu@gmail.com";
            student.Phone = "9876543210";

            Console.WriteLine("Id: "+student.Id);
            Console.WriteLine("Name: " + student.Name);
            Console.WriteLine("DateofBirth: " + student.DateofBirth);
            Console.WriteLine("Email: " + student.Email);
            Console.WriteLine("Phone: " + student.Phone);
        }
    }
}
