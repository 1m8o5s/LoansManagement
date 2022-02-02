using Microsoft.Azure.Cosmos;

namespace Loans.Helpers.CosmosDb
{
    public interface ICosmosClientFactory
    {
        public CosmosClient NewCosmosClient(string url, string primaryKey);
    }
}
