using Loans.Domain.Entities;
using Loans.Domain.Models;
using Loans.Service.Calculations.Contracts;
using System;

namespace Loans.Service.Calculations
{
    public class FloatingRateCalculationMethod : ICalculationMethod
    {
        private const int MAX_FLEXIBLE_RATE_PERCENT = 3;

        private const int PERCENT_CHANGE_INTERVAL = 3;

        private const double PERCENT_CHANGE_VALUE = 0.25;

        public PaymentItem CalculateInitialPaymentItem(LoanAddModel loan)
        {
            int month = 1;

            double balance = loan.LoanSum;

            double interest = balance * (loan.Interest - GetFlexibleRate(month)) / 100 / 12;

            double payment = loan.LoanSum / loan.Term;

            double total = payment + interest;

            return new PaymentItem
            {
                Month = month,
                Balance = balance,
                Interest = interest,
                Payment = payment,
                Total = total
            };
        }

        public PaymentItem CalculateNextPaymentItem(PaymentItem previousPaymentItem, LoanAddModel model)
        {
            int month = previousPaymentItem.Month + 1;

            double balance = previousPaymentItem.Balance - previousPaymentItem.Payment;

            double interest = balance * (model.Interest - GetFlexibleRate(month)) / 100 / 12;

            double payment = model.LoanSum / model.Term;

            double total = payment + interest;

            return new PaymentItem
            {
                Month = month,
                Balance = balance,
                Interest = interest,
                Payment = payment,
                Total = total
            };
        }

        private double GetFlexibleRate(int monthNumber)
        {
            double percent = PERCENT_CHANGE_VALUE * (int)(monthNumber / PERCENT_CHANGE_INTERVAL);
            return Math.Min(percent, MAX_FLEXIBLE_RATE_PERCENT);
        }
    }
}
