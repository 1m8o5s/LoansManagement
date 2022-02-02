namespace Loans.Helpers.Validation.Contracts
{
    public interface IValidatorBuilderFactory
    {
        public IValidatorBuilder<T> NewValidatorBuilder<T>();
    }
}
