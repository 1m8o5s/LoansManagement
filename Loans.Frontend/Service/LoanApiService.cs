using Loans.Frontend.Common;
using Loans.Frontend.Models;
using Loans.Frontend.Service.Contracts;
using System.Text.Json;

namespace Loans.Frontend.Service
{
    public class LoanApiService : BaseApiService, ILoanApiService
    {
        public LoanApiService(HttpClient httpClient, INotificationService notificationService) : base(httpClient, notificationService) { }

        public async Task<HttpSuccessResponseModel> AddLoan(LoanAddModel loanToAdd, string authToken)
        {
            HttpSuccessResponseModel response = await MakePostRequest<LoanAddModel>(ApiEndpoints.LOAN_ADD_ENDPOINT, loanToAdd, authToken);

            if (response.Success)
            {
                response.HttpResponseModel.Data = ((JsonElement)response.HttpResponseModel.Data).Deserialize<Guid>();
            } else
            {
                await NotificationService.ShowErrorNotification("Error while adding loan");
            }

            return response;
        }

        public async Task<HttpSuccessResponseModel> GetLoanInformation(Guid id, string authToken)
        {
            Dictionary<string, Guid> payload = new Dictionary<string, Guid> {{ "id", id }};

            HttpSuccessResponseModel response = await MakeGetRequest<Guid>(ApiEndpoints.LOAN_GET_ENDPOINT, payload, authToken);

            if (response.Success)
            {
                response.HttpResponseModel.Data = ((JsonElement)response.HttpResponseModel.Data).Deserialize<LoanInformationModel>();
            }
            else
            {
                await NotificationService.ShowErrorNotification("Error while getting loan information");
            }

            return response;
        }

        public async Task<HttpSuccessResponseModel> GetLoansList(string authToken)
        {
            HttpSuccessResponseModel response = await MakeGetRequest(ApiEndpoints.LOANS_LIST_GET_ENDPOINT, authToken);

            if (response.Success)
            {
                response.HttpResponseModel.Data = ((JsonElement)response.HttpResponseModel.Data).Deserialize<List<LoanListModel>>();
            } else
            {
                await NotificationService.ShowErrorNotification("Error while getting loan list");
            }

            return response;
        }
    }
}
