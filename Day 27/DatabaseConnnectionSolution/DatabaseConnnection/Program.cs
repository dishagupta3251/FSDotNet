using System.Data;
using Microsoft.Data.SqlClient;

namespace DatabaseConnnection
{
    internal class Program
    {
        SqlConnection conn = new SqlConnection("server=5CD413DKR1\\DEMOINSTANCE;Integrated Security=true;Initial catalog=Northwind;TrustServerCertificate=true");
        public Program()
        {
            
        }
        void GetData()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from Products";
            try
            {
                conn.Open();
                Console.WriteLine("Connection open");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["ProductName"]);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        void AddData()
        {
            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            Console.WriteLine("enter password");
            string password = Console.ReadLine();
            string insertQuery = $"Insert into Users values('{username}','{password}'); Select * from Users";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            try {
                conn.Open();
                //int rowsAffected= cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
               // if (rowsAffected > 0) { Console.WriteLine("User created"); }
                //else { Console.WriteLine("Not executed"); }
                while (reader.Read()) { Console.WriteLine($"{reader["username"]},{reader["password"]}"); }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
        }
        void DeleteUser()
        {
            Console.WriteLine("Enter username to be deleted");
            string deleteUsername = Console.ReadLine();
            string deleteQuery = $"delete from Users where username=@user; select * from users";
            SqlCommand cmd = new SqlCommand(deleteQuery, conn);
            
            try
            { 
                conn.Open();
                cmd.Parameters.AddWithValue("@user",deleteUsername);
               

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Console.WriteLine(r["username"]);
                    Console.WriteLine(r["password"]);
                    Console.WriteLine("-----------------------------------");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }
            }
        void DisconnectedArchitecture()
        {
           
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("Select * from Categories",conn);
            
            DataSet dataSet = new DataSet();
            try {
                
                adapter.Fill(dataSet);
                
                foreach (DataRow data in dataSet.Tables[0].Rows) {
                    Console.Write("Category Name: "+data["CategoryName"]);
                    Console.WriteLine("  Description: " + data["Description"]);
                    Console.WriteLine("-------------------------------------");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
           
        }
        static void Main(string[] args)
        {
           Program  program = new Program();
            // program.GetData();  
            program.AddData();
            //program.DisconnectedArchitecture();
            program.DeleteUser();
        }
    }
}
