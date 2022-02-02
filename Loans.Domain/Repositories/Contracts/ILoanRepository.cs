using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loans.Domain.Entities;

namespace Loans.Domain.Repositories.Contracts
{
    public interface ILoanRepository {
        public Task<Loan> AddLoan(Loan loan);

        public Task<Loan> GetLoanById(Guid id);

        public Task<List<Loan>> GetAllLoans();
    }
}