using System;
using System.Collections.Generic;
using System.Linq;
using Loans.Helpers.EnumHelper.Contracts;

namespace Loans.Helpers.EnumHelper
{
    public class EnumHelper<T> : IEnumHelper<T> where T : Enum
    {
        public IEnumerable<T> GetEnumValues() => 
            Enum.GetValues(typeof(T)).Cast<T>();
    }
}
