using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loans.Domain.Entities;
using Loans.Domain.Models;

namespace Loans.Service.Data.Contracts
{
    public interface ILoanService
    {
        public Task<Loan> AddLoanWithCalculatedPaymentGraphs(LoanAddModel loanModel, List<PaymentItem> paymentItems);

        public Task<Loan> GetLoan(Guid id);

        public Task<List<LoanListModel>> GetAllLoans();

        public Task<LoanListModel> GetLoanInfo(Guid id);
    }
}
