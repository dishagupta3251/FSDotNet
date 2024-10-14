using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Database
{
    internal interface IUserServices
    {
        public void Register_User();
        public string Login_User();

        public void UpdatePassword(string username);
    }
}
