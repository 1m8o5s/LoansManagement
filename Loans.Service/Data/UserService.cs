using Loans.Domain.Entities.Identity;
using Loans.Domain.Models;
using Loans.Helpers.Validation;
using Loans.Helpers.Validation.Contracts;
using Loans.Service.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Loans.Service.Data
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IValidatorBuilderFactory _validatorBuilderFactory;

        public UserService(
            UserManager<User> userManager,
            IValidatorBuilderFactory validatorBuilderFactory
            )
        {
            _userManager = userManager; 
            _validatorBuilderFactory = validatorBuilderFactory;
        }

        public async Task AddUserAsync(UserAuthenticateModel userModel)
        {
            Validator<UserAuthenticateModel> validator = _validatorBuilderFactory
                    .NewValidatorBuilder<UserAuthenticateModel>()
                    .AddRule(model => model != null)
                    .AddRule(model => !string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.Password))
                    .Build();

            if (!validator.IsValid(userModel))
            {
                throw new Exception("Not valid user model");
            }

            User user = new User
            {
                Name = userModel.Name,
                UserName = userModel.Name,
                Email = $"test{userModel.Name}@localhost"
            };

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Not valid user model");
            }
        }

        public User GetUserByName(string name)
        {
            User user = _userManager.Users.SingleOrDefault(x => x.Name == name);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public User GetUserByUserModel(UserAuthenticateModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.Name) || string.IsNullOrEmpty(userModel.Password))
            {
                throw new Exception("Invalid user name or password");
            }

            User user = GetUserByName(userModel.Name);

            return user;
        }

        public User GetUserForGenerateToken(UserAuthenticateModel userModel)
        {
            User user = GetUserByUserModel(userModel);

            Validator<User> validator = _validatorBuilderFactory
                   .NewValidatorBuilder<User>()
                   .AddRule(model => model != null && _userManager.CheckPasswordAsync(user, userModel.Password).Result)
                   .AddRule(model => model.TokenExpires == default(DateTime) || model.TokenExpires < DateTime.UtcNow)
                   .Build();

            if (!validator.IsValid(user))
            {
                throw new Exception("User cant be choosen for generate token");
            }

            return user;
        }

        public async Task UpdateTokenExpiringDateForUserAsync(User user, DateTime expiringTime)
        {
            user.TokenExpires = expiringTime;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Unsuccessful try to update token expire time");
            }
        }
    }
}
