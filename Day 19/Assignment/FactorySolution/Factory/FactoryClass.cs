using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    internal class FactoryClass : IFactoryClass
    {
        public Account Create { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static Account CreateAccount(string accountType)
        { 
            Account account = null;
            string choice = null;
            do
            {

        choice = Console.ReadLine();
                switch (choice.ToLower())
                {
                    case "current":
                        account = new CurrentAccount();
                        break;
                    case "saving":
                        account = new SavingAccount();
                        break;
                    case "business":
                        account = new BusinessAccount();
                        break;
                }
            } while (choice != null);
            return account;
        }
      }
    }

