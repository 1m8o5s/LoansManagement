using Loans.Domain.Entities.Identity;
using Loans.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Loans.Service.Data.Contracts
{
    public interface IUserService
    {
        public Task AddUserAsync(UserAuthenticateModel userModel);

        public User GetUserByName(string name);

        public User GetUserByUserModel(UserAuthenticateModel userModel);

        public User GetUserForGenerateToken(UserAuthenticateModel userModel);

        public Task UpdateTokenExpiringDateForUserAsync(User user, DateTime expiringTime);

    }
}
