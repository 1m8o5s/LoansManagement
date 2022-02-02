using Loans.Frontend.Models;

namespace Loans.Frontend.Authentication.Contracts
{
    public interface IAuthState
    {
        public event Action<bool> OnAuthStateChanged;

        public Task LogIn(TokenWithExpireDateModel tokenWithExpireDate);

        public Task LogOut();

        public Task<CookieValueModel> GetToken();

        public Task<bool> IsAuthorized();
    }
}
