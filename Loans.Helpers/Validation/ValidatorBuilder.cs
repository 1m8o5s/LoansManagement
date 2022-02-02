using System;
using Loans.Helpers.Validation.Contracts;

namespace Loans.Helpers.Validation
{
    public class ValidatorBuilder<T> : IValidatorBuilder<T>
    {
        private Validator<T> _validator;

        public ValidatorBuilder()
        {
            _validator = new Validator<T>();
        }

        public Validator<T> Build()
        {
            Validator<T> validatorToReturn = _validator;

            _validator = new Validator<T>();
            
            return validatorToReturn;
        }

        public IValidatorBuilder<T> AddRule(Predicate<T> rule)
        {
            _validator.Add(rule);

            return this;
        }
    }
}
