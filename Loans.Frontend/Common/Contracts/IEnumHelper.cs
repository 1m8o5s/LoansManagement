namespace Loans.Frontend.Common.Contracts
{
    public interface IEnumHelper<T> where T : Enum
    {
        public IEnumerable<T> GetEnumValues();
    }
}
