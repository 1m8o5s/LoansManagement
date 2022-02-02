using Loans.Frontend.Common.Contracts;

namespace Loans.Frontend.Common
{
    public class EnumHelperFactory : IEnumHelperFactory
    {
        IEnumHelper<T> IEnumHelperFactory.NewEnumHelper<T>() =>
            new EnumHelper<T>();
    }
}
