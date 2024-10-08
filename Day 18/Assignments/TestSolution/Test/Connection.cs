using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//private constructor
namespace Test
{
    internal class Connection
    {
        public string Name {  get; set; }
        private static Connection obj = null;
        private Connection() { }

        public static Connection SetName()
        {
              obj= new Connection();
              obj.Name="Disha";
              return obj;

        }
    }
}

