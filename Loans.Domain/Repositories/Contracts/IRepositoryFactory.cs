using Microsoft.Azure.Cosmos;

namespace Loans.Domain.Repositories.Contracts
{
    public interface IRepositoryFactory
    {
        public ILoanRepository NewLoanRepository(CosmosClient client);
    }
}
