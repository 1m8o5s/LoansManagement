using Loans.Helpers.Validation.Contracts;

namespace Loans.Helpers.Validation
{
    public class ValidatorBuilderFactory : IValidatorBuilderFactory
    {
        public IValidatorBuilder<T> NewValidatorBuilder<T>() 
            => new ValidatorBuilder<T>();
    }
}
