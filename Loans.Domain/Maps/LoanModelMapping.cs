using Loans.Domain.Entities;
using Loans.Domain.Models;

namespace Loans.Domain.Maps
{
    public static class LoanModelMapping
    {
        public static Loan ToLoan(this LoanAddModel model) =>
            new Loan
            {
                LoanSum = model.LoanSum,
                Customer = model.Customer,
                Interest = model.Interest,
                Term = model.Term,
                Type = model.Type,
            };

        public static LoanListModel ToListModel(this Loan loan) =>
            new LoanListModel
            {
                Id = loan.Id,
                LoanSum = loan.LoanSum,
                Customer = loan.Customer,
                Interest = loan.Interest,
                Term = loan.Term,
                Type = loan.Type,
            };
    }
}
