using Microsoft.EntityFrameworkCore;
using BankingCustomerManagement.Models;

namespace BankingCustomerManagement.Context
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions dbContextOption) : base(dbContextOption)
        {

        }
       
        public DbSet<Customer> Customers { get; set; }

       
    }
  }
