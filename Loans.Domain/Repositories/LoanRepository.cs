using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loans.Domain.Entities;
using Loans.Domain.Repositories.Contracts;
using Microsoft.Azure.Cosmos;

namespace Loans.Domain.Repositories
{
    public class LoanRepository : CosmosBaseRepository, ILoanRepository {
        private const string DATABASE_ID = "loans";

        private const string CONTAINER_ID = "loans";

        public LoanRepository(CosmosClient client) : base(client, DATABASE_ID, CONTAINER_ID) {}

        public async Task<Loan> AddLoan(Loan loan) {

            if (loan.Id == default)
            {
                loan.Id = Guid.NewGuid();
            }

            Loan addedLoan = await Container.CreateItemAsync(loan, new PartitionKey(loan.Customer));

            return addedLoan;
        }

        public async Task<Loan> GetLoanById(Guid id) {

            string sqlQuery = $"SELECT * FROM loans WHERE loans.id=\"{id.ToString()}\"";

            FeedResponse<Loan> loans = await GetQueryResult<Loan>(sqlQuery);

            return loans.Resource.FirstOrDefault();
        }

        public async Task<List<Loan>> GetAllLoans()
        {
            string sqlQuery = "SELECT * FROM loans";

            FeedResponse<Loan> loans = await GetQueryResult<Loan>(sqlQuery);

            return loans.Resource.ToList();
        }
    }
}