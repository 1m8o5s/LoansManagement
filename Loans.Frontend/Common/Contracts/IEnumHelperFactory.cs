namespace Loans.Frontend.Common.Contracts
{
    public interface IEnumHelperFactory
    {
        IEnumHelper<T> NewEnumHelper<T>() where T: Enum;
    }
}
