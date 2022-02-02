using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Loans.Domain.Repositories
{
    public abstract class CosmosBaseRepository
    {
        protected CosmosClient Client {get;}

        protected Container Container {get;}

        protected CosmosBaseRepository(CosmosClient client, string databaseId, string containerId) {
            Client = client;

            Container = Client.GetContainer(databaseId, containerId);
        }

        protected async Task<FeedResponse<T>> GetQueryResult<T>(string query)
        {
            try
            {
                QueryDefinition queryDefinition = new QueryDefinition(query);

                FeedIterator<T> queryIterator = Container.GetItemQueryIterator<T>(queryDefinition);

                FeedResponse<T> queryResult = await queryIterator.ReadNextAsync();

                return queryResult;
            }
            catch (Exception exc)
            {
                ILogger<CosmosBaseRepository> logger = new LoggerFactory().CreateLogger<CosmosBaseRepository>();
                
                logger.LogError($"Error while executing query {exc.Message}");

                throw;
            }
        }
    }
}