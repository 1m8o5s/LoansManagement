using System;
using Loans.Domain.Entities;
using Loans.Domain.Models;
using Loans.Service.Calculations.Contracts;

namespace Loans.Service.Calculations
{
    public class UnsupportedCalculationMethod : ICalculationMethod
    {
        public PaymentItem CalculateInitialPaymentItem(LoanAddModel loan)
        {
            throw new InvalidOperationException();
        }

        public PaymentItem CalculateNextPaymentItem(PaymentItem previousPaymentItem, LoanAddModel model)
        {
            throw new InvalidOperationException();
        }
    }
}
