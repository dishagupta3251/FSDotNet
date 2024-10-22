using EFCoreWebAPI.Interfaces;

namespace EFCoreWebAPI.Services
{
    public class CalculateService : ICalculate
    {
        public int Add(int num1, int num2)
        {
            int result = num1 + num2;
            return result;
        }
    }
}