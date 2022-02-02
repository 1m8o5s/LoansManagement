using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loans.Domain.Entities;
using Loans.Domain.Maps;
using Loans.Domain.Models;
using Loans.Domain.Repositories.Contracts;
using Loans.Helpers.CosmosDb;
using Loans.Service.Data.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Loans.Service.Data
{
    public class LoanService : ILoanService
    {
        private readonly ICosmosClientFactory _clientFactory;

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly string _cosmosDbUrl;
        private readonly string _cosmosDbPrimaryKey;

        public LoanService(ICosmosClientFactory clientFactory,
                           IRepositoryFactory repositoryFactory,
                           IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _repositoryFactory = repositoryFactory;
            _cosmosDbUrl = configuration.GetSection("CosmosDbUrl").Value;
            _cosmosDbPrimaryKey = configuration.GetSection("CosmosDbPrimaryKey").Value;
        }

        public async Task<Loan> AddLoanWithCalculatedPaymentGraphs(LoanAddModel loanAddModel, List<PaymentItem> paymentItems)
        {
            Loan loan = loanAddModel.ToLoan();
            loan.PaymentGraph = paymentItems;

            using (CosmosClient cosmosDbClient = _clientFactory.NewCosmosClient(_cosmosDbUrl, _cosmosDbPrimaryKey))
            {
                ILoanRepository loanRepository = _repositoryFactory.NewLoanRepository(cosmosDbClient);

                Loan addedLoan = await loanRepository.AddLoan(loan);

                return addedLoan;
            }
        }

        public async Task<List<LoanListModel>> GetAllLoans()
        {

            using (CosmosClient cosmosDbClient = _clientFactory.NewCosmosClient(_cosmosDbUrl, _cosmosDbPrimaryKey))
            {
                ILoanRepository loanRepository = _repositoryFactory.NewLoanRepository(cosmosDbClient);

                List<LoanListModel> loansToReturn = (await loanRepository.GetAllLoans()).Select(loan => loan.ToListModel()).ToList();

                return loansToReturn;
            }
        }

        public async Task<Loan> GetLoan(Guid id)
        {
            using (CosmosClient cosmosDbClient = _clientFactory.NewCosmosClient(_cosmosDbUrl, _cosmosDbPrimaryKey))
            {
                ILoanRepository loanRepository = _repositoryFactory.NewLoanRepository(cosmosDbClient);

                Loan loanToReturn = await loanRepository.GetLoanById(id);

                return loanToReturn;
            }
        }

        public async Task<LoanListModel> GetLoanInfo(Guid id)
        {
            using (CosmosClient cosmosDbClient = _clientFactory.NewCosmosClient(_cosmosDbUrl, _cosmosDbPrimaryKey))
            {
                ILoanRepository loanRepository = _repositoryFactory.NewLoanRepository(cosmosDbClient);

                Loan loanToReturn = await loanRepository.GetLoanById(id);

                return loanToReturn.ToListModel();
            }
        }
    }
}
