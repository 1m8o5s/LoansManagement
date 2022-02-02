namespace Loans.Service.Calculations.CalculationPipeline.Contracts
{
    public interface IPaymentGraphGeneratorFactory
    {
        public IPaymentGraphGenerator NewPaymentGenerator();
    }
}
