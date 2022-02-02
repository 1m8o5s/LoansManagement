using System;

namespace Loans.Helpers.EnumHelper.Contracts
{
    public interface IEnumHelperFactory
    {
        IEnumHelper<T> NewEnumHelper<T>() where T: Enum;
    }
}
