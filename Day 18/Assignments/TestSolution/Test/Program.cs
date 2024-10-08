namespace Test
{
    internal class Program
    {
        void Check_Destructor()
        {
            using (Destructor destructor = new Destructor())
            {
                Console.WriteLine("Using the resource");
            } 
        }
        
        void Call()
        {
            //indexer
            Test t =new Test();
            Console.WriteLine(t[0]);
            Console.WriteLine(t["one"]);
        }
        void HandleProduct()
        {
            //operator overloading
            Product p = new Product() { Id=101,Name="mobile",Price=298328};
            Product product = new Product() { Id = 102, Name = "laptop", Price = 289383 };
            Console.WriteLine(p + product);
        }
        void PrivateConstructor()
        {
            //private constructor
            Connection obj1 = Connection.SetName();
            Connection obj2 = Connection.SetName();
            //obj2 will print same as only singleton, that is one object is made
            Console.WriteLine(obj1.Name+" "+obj2.Name);
            obj1.Name = "Dishag";
            Console.WriteLine(obj1.Name);

        }
        void InnerClass()
        {
            //inner class
            Product obj = new Product();
            Console.WriteLine("Discounted price is "+obj.Calculate(800));
            
        }
        void Static_Constructor()
        {
            //static constructor
            //without object creation, using static constructor
            string name=Company.Name;
            Console.WriteLine(name);
            // using public constructor
            Company company = new Company();
            Console.WriteLine(Company.Name);

        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Call();
            program.HandleProduct();
            program.PrivateConstructor();
            program.InnerClass();
            program.Static_Constructor();
        }
    }
}
