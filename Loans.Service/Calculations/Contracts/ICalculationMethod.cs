using Loans.Domain.Entities;
using Loans.Domain.Models;

namespace Loans.Service.Calculations.Contracts
{
    public interface ICalculationMethod
    {
        public PaymentItem CalculateInitialPaymentItem(LoanAddModel loan);

        public PaymentItem CalculateNextPaymentItem(PaymentItem previousPaymentItem, LoanAddModel model);
    }
}
