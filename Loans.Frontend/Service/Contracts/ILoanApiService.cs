using Loans.Frontend.Models;

namespace Loans.Frontend.Service.Contracts;

public interface ILoanApiService
{
    public Task<HttpSuccessResponseModel> AddLoan(LoanAddModel loanToAdd, string authToken);

    public Task<HttpSuccessResponseModel> GetLoanInformation(Guid id, string authToken);

    public Task<HttpSuccessResponseModel> GetLoansList(string authToken);
}