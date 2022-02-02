using Loans.Domain.Common;
using Loans.Service.Calculations.Contracts;

namespace Loans.Service.Calculations
{
    public class CalculationMethodFactory : ICalculationMethodFactory
    {
        public ICalculationMethod NewCalculationMethod(CalculationType type) =>
            type switch
            {
                CalculationType.Annuity => new AnnuityCalculationMethod(),
                CalculationType.EqualPricing => new EqualPrincipalityCalculationMethod(),
                CalculationType.FloatingRate => new FloatingRateCalculationMethod(),
                _ =>  new UnsupportedCalculationMethod()
            };
    }
}
