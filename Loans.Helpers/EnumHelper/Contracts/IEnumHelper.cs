using System;
using System.Collections.Generic;

namespace Loans.Helpers.EnumHelper.Contracts
{
    public interface IEnumHelper<T> where T : Enum
    {
        public IEnumerable<T> GetEnumValues();
    }
}
