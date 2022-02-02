using Loans.Frontend.Common.Contracts;

namespace Loans.Frontend.Common
{
    public class EnumHelper<T> : IEnumHelper<T> where T : Enum
    {
        public IEnumerable<T> GetEnumValues() => 
            Enum.GetValues(typeof(T)).Cast<T>();
    }
}
