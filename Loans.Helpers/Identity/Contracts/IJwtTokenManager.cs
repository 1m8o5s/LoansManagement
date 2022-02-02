using Loans.Domain.Entities.Identity;
using Loans.Domain.Models;

namespace Loans.Helpers.Identity.Contracts
{
    public interface IJwtTokenManager
    {
        public TokenWithExpireDateModel GenerateJwtToken(User user, Role role);

        public string GetUserNameAuthorizedByToken(string token);
    }
}
