using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace Loans.Helpers.CosmosDb
{
    public class CosmosClientFactory : ICosmosClientFactory
    {
        public CosmosClient NewCosmosClient(string url, string primaryKey)
        {
            CosmosClientBuilder client = new CosmosClientBuilder(url, primaryKey);

            return client.WithConnectionModeDirect()
                .Build();
        }
    }
}
