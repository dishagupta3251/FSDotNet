using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    //Destructor

    internal class Payment : PaymentMethod
    {
        public Payment(double amount) : base(amount)
        {
        }

        public override void ProcessPayment()
        {
            throw new NotImplementedException();
        }
    }
    abstract class PaymentMethod (double amount)
    {

    public abstract void ProcessPayment();
    public void CompletePayment()
    {
        Console.WriteLine("Payment completed.");
    }
}
