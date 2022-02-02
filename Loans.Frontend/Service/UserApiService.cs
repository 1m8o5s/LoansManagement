using Loans.Frontend.Common;
using Loans.Frontend.Models;
using Loans.Frontend.Service.Contracts;
using System.Text.Json;

namespace Loans.Frontend.Service;

public class UserApiService : BaseApiService, IUserApiService
{
    public UserApiService(HttpClient httpClient, INotificationService notificationService) : base(httpClient, notificationService) { }
    
    public async Task<HttpSuccessResponseModel> Authenticate(UserAuthenticateModel model)
    {
        HttpSuccessResponseModel response = await MakePostRequest<UserAuthenticateModel>(ApiEndpoints.LOG_IN_ENDPOINT, model);

        if (response.Success)
        {
            response.HttpResponseModel.Data = ((JsonElement)response.HttpResponseModel.Data).Deserialize<TokenWithExpireDateModel>();
        } else
        {
            await NotificationService.ShowErrorNotification("Error while logging in");
        }

        return response;

    }

    public async Task<HttpSuccessResponseModel> LogOut(string token)
    {
        HttpSuccessResponseModel response = await MakePostRequest<UserAuthenticateModel>(ApiEndpoints.LOG_OUT_ENDPOINT, null, token);

        if (!response.Success)
        {
            await NotificationService.ShowErrorNotification("Error while logging out");
        }

        return response;
    }

    public async Task<HttpSuccessResponseModel> SignUp(UserAuthenticateModel model)
    {
        HttpSuccessResponseModel response = await MakePostRequest<UserAuthenticateModel>(ApiEndpoints.SIGN_UP_ENDPOINT, model);

        if (response.Success)
        {
            await NotificationService.ShowSuccessNotification("Successful signing up");
        } else
        {
            await NotificationService.ShowErrorNotification("Error while signing up");
        }

        return response;
    }
}