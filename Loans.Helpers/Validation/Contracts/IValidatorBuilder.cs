using System;

namespace Loans.Helpers.Validation.Contracts
{
    public interface IValidatorBuilder<T>
    {
        public IValidatorBuilder<T> AddRule(Predicate<T> rule);

        public Validator<T> Build();
    }
}
