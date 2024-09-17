using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion
{
    public class EmployeePromotion:IComparable<EmployeePromotion>
    {
        public string Name { get; set; }
        public EmployeePromotion() { }
        public EmployeePromotion(string name) { 
            Name = name;
        }
        public override string ToString() { 
            return Name;
        }

        public int CompareTo(EmployeePromotion? other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
