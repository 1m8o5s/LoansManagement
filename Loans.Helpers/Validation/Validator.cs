using System;
using System.Collections.Generic;
using System.Linq;

namespace Loans.Helpers.Validation
{
    public class Validator<T>
    {
        private readonly List<Predicate<T>> _rulesToCheck;

        public Validator() {
            _rulesToCheck = new List<Predicate<T>>();
        }

        public void Add(Predicate<T> item)
        {
            _rulesToCheck.Add(item);
        }

        public bool IsValid(T model)
        {
            return _rulesToCheck.All(rule => rule.Invoke(model));
        }
    }
}
