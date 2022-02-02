using Loans.Domain.Common;

namespace Loans.Service.Calculations.Contracts
{
    public interface ICalculationMethodFactory
    {
        public ICalculationMethod NewCalculationMethod(CalculationType type);
    }
}
