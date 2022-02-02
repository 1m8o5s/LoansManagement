using Loans.Domain.Entities;
using Loans.Domain.Models;
using System;
using Loans.Service.Calculations.Contracts;

namespace Loans.Service.Calculations
{
    public class EqualPrincipalityCalculationMethod : ICalculationMethod
    {
        public PaymentItem CalculateInitialPaymentItem(LoanAddModel loan)
        {
            double k1 = loan.Interest / 12 / 100;
            double balance = loan.LoanSum;
            double interest = balance * k1;
            double payment = loan.LoanSum / loan.Term;
            double total = interest + payment;

            return new PaymentItem
            {
                Month = 1,
                Balance = Math.Round(balance, 2),
                Interest = Math.Round(interest, 2),
                Total = Math.Round(total, 2),
                Payment = Math.Round(payment, 2)
            };
        }

        public PaymentItem CalculateNextPaymentItem(PaymentItem previousPaymentItem, LoanAddModel model)
        {
            (double k1, double k2, double k3) = CalculateTemporaryVariables(model);

            double balance = previousPaymentItem.Balance - previousPaymentItem.Payment;
            double interest = balance * k1;
            double payment = model.LoanSum / model.Term;
            double total = payment + interest;

            return new PaymentItem
            {
                Month = previousPaymentItem.Month + 1,
                Balance = Math.Round(balance, 2),
                Interest = Math.Round(interest, 2),
                Total = Math.Round(total, 2),
                Payment = Math.Round(payment, 2)
            };
        }

        private Tuple<double, double, double> CalculateTemporaryVariables(LoanAddModel loan)
        {
            double k1 = loan.Interest / 12 / 100;
            double k2 = Math.Pow(1 + k1, loan.Term) - 1;
            double k3 = k1 + (k1 / k2);

            return new Tuple<double, double, double>(k1, k2, k3);
        }
    }
}
