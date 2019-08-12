using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;

namespace UsersRestApi.Domain
{
    public class CustomerName : ValueObject
    {
        private CustomerName(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static Result<CustomerName> Create(string value)
        {
            return Result.Ok<CustomerName>(new CustomerName(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
