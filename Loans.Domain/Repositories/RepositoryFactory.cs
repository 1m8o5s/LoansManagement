using Microsoft.Azure.Cosmos;
using Loans.Domain.Repositories.Contracts;

namespace Loans.Domain.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public ILoanRepository NewLoanRepository(CosmosClient client)
            => new LoanRepository(client);
    }
}
