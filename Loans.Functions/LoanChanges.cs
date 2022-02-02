using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loans.Domain.Models;
using Loans.Service.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace Loans.Functions
{
    public class LoanChanges : ServerlessHub
    {
        private readonly ILoanService _loanService;

        public LoanChanges(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [FunctionName(nameof(Broadcast))]
        public async Task Broadcast([CosmosDBTrigger(
            databaseName: "loans",
            collectionName: "loans",
            ConnectionStringSetting = "CosmosDbConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists=true)]IReadOnlyList<Document> addedOrChangedDocuments,
            [SignalR(HubName = "list")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {
            try
            {
                if (addedOrChangedDocuments != null && addedOrChangedDocuments.Count > 0)
                {
                    foreach (Document document in addedOrChangedDocuments)
                    {
                        LoanListModel changedLoan;

                        try
                        {
                            changedLoan = await _loanService.GetLoanInfo(Guid.Parse(document.Id));
                        }
                        catch (Exception exc)
                        {
                            log.LogError($"Changes in cosmos db cant be fetched {exc.Message}");
                            continue;
                        }

                        await signalRMessages.AddAsync(new SignalRMessage
                        {
                            Target = "loan",
                            Arguments = new object[] { changedLoan }
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                log.LogError($"Error while loading changes from cosmos db {exc.Message}");
            }
        }

        [FunctionName(nameof(Negotiate))]
        public SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest request,
            [SignalRConnectionInfo(HubName = "list")] SignalRConnectionInfo connectionInfo
            )
        {
            return connectionInfo;
        }
    }
}
