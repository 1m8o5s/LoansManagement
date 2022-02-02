using Loans.Domain.Entities;
using Loans.Domain.Models;
using System.Collections.Generic;
using Loans.Service.Calculations.Contracts;

namespace Loans.Service.Calculations.CalculationPipeline.Contracts
{
    public interface IPaymentGraphGenerator
    {
        public IEnumerable<PaymentItem> GeneratePaymentItems(ICalculationMethod calculationMethod, LoanAddModel loanToGenerateGraph);
    }
}
