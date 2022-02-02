using Loans.Frontend.Models;

namespace Loans.Frontend.Service.Contracts;

public interface IUserApiService
{
    public Task<HttpSuccessResponseModel> Authenticate(UserAuthenticateModel model);

    public Task<HttpSuccessResponseModel> SignUp(UserAuthenticateModel model);

    public Task<HttpSuccessResponseModel> LogOut(string token);
}