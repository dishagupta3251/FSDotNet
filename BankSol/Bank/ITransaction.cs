using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public interface ITransaction
    {
        public void withdraw(double minBalance, int percent, double amount) { }
        public void add(int percent, double amount) { }
    }
}
