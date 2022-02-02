using Loans.Service.Calculations.CalculationPipeline.Contracts;
using Loans.Helpers.Validation.Contracts;

namespace Loans.Service.Calculations.CalculationPipeline
{
    public class PaymentGraphGeneratorFactory : IPaymentGraphGeneratorFactory
    {
        private readonly IValidatorBuilderFactory _validatorBuilderFactory;

        public PaymentGraphGeneratorFactory(IValidatorBuilderFactory validatorBuilderFactory)
        {
            _validatorBuilderFactory = validatorBuilderFactory;
        }

        public IPaymentGraphGenerator NewPaymentGenerator() =>
            new PaymentGraphGenerator(_validatorBuilderFactory);
    }
}
