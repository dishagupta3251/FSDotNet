using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace Assignment_Database
{
    internal class UserServices : IUserServices
    {
        SqlConnection connnection = new SqlConnection("server=5CD413DKR1\\DEMOINSTANCE;Integrated Security=true;Initial catalog=Northwind;TrustServerCertificate=true;");
        public string Login_User()
        {
            
            Console.Write("Username: ");
            string username=Console.ReadLine();
            Console.Write("Password: ");
            string password=Console.ReadLine();
            string loginQuery = "Select * from Users where username=@user and password=@pass";
            SqlCommand cmd = new SqlCommand(loginQuery,connnection);
            
            try {
                connnection.Open();
                cmd.Parameters.AddWithValue("user", username);
                cmd.Parameters.AddWithValue("pass", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { Console.WriteLine("Login Successful");
                    Console.WriteLine();
                }
                else {
                    username = null;
                    Console.WriteLine("Login failed"); }
               
                

            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            finally {
                connnection.Close();
            }
            return username;   
        }

        public void Register_User()
        {
            User user =new User();
            Console.WriteLine("Enter your Name: ");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter your Username: ");
            user.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            user.Password = Console.ReadLine();
            string insertQuery = $"Insert into Users values('{user.Name}','{user.UserName}','{user.Password}')";
            SqlCommand cmd=new SqlCommand(insertQuery, connnection);
            try {
                connnection.Open();
                int rowsAffected=cmd.ExecuteNonQuery();
                if (rowsAffected > 0) Console.WriteLine("User added Successfully");
                else Console.WriteLine("Registration failed");

            }
            catch (Exception ex){ Console.WriteLine(ex.Message); }
            finally { connnection.Close(); }

        }
        bool CheckUser(string username, string password)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM Users WHERE Username=@un AND Password=@pass", connnection);
            try
            {
                connnection.Open();
                sqlCommand.Parameters.AddWithValue("@un", username);
                sqlCommand.Parameters.AddWithValue("@pass", password);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connnection.Close();
            }

        }

        public void UpdatePassword(string username)
        {
            
            Console.WriteLine("Pleae enter your current password");
            string password = Console.ReadLine();
            try
            {
                if (CheckUser(username, password))
                {
                    Console.WriteLine("Please enter your new password");
                    string newPassword = Console.ReadLine();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Users SET Password=@newpass WHERE Username=@un", connnection);
                    sqlCommand.Parameters.AddWithValue("@newpass", newPassword);
                    sqlCommand.Parameters.AddWithValue("@un", username);
                    try
                    {
                        connnection.Open();
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Password updated successfully");
                        }
                        else
                        {
                            Console.WriteLine("Password update failed");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Could not verify user. Sorry cannot update password now.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connnection.Close();
            }
        }


    }
}

