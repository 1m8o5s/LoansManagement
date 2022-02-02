using Loans.Helpers.EnumHelper.Contracts;

namespace Loans.Helpers.EnumHelper
{
    public class EnumHelperFactory : IEnumHelperFactory
    {
        IEnumHelper<T> IEnumHelperFactory.NewEnumHelper<T>() =>
            new EnumHelper<T>();
    }
}
