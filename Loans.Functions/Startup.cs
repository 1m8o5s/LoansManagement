using Loans.Helpers.CosmosDb;
using Loans.Helpers.Validation;
using Loans.Helpers.Validation.Contracts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Loans.Service.Data;
using Loans.Service.Calculations.CalculationPipeline;
using Loans.Service.Calculations.CalculationPipeline.Contracts;
using Loans.Service.Data.Contracts;
using Loans.Domain.Repositories.Contracts;
using Loans.Domain.Repositories;

[assembly: FunctionsStartup(typeof(Loans.Functions.Startup))]
namespace Loans.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();

            builder.Services.AddScoped<ICosmosClientFactory, CosmosClientFactory>();
            
            builder.Services.AddScoped<ILoanService, LoanService>();

            builder.Services.AddScoped<IPaymentGraphGeneratorFactory, PaymentGraphGeneratorFactory>();

            builder.Services.AddScoped<IValidatorBuilderFactory, ValidatorBuilderFactory>();
        }
    }
}
