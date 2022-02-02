using Loans.Frontend.Models;

namespace Loans.Frontend.Common.Contracts
{
    public interface ICookieHelper
    {
        public Task<CookieValueModel> GetCookieValue(string cookieName);

        public Task AddCookie(string cookieName, string cookieValue, DateTime expires);

        public Task Clear(string cookieName);
    }
}
