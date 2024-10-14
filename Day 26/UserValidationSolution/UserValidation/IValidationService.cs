using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UserValidation
{
    internal interface IValidationService
    {
        public Customer GetCustomer();
        public bool VerifyEmail(string email);
        public void GetAllValidCustomer(Customer customer);
        public string GenerateOTP();
        public bool VerifyOTP(string otp);
        public bool SaveCustomerData(Customer customer,string otp);





    }
}
