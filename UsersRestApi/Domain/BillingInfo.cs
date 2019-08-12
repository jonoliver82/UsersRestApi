using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;

namespace UsersRestApi.Domain
{
    public class BillingInfo : ValueObject
    {
        private BillingInfo(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static Result<BillingInfo> Create(string value)
        {
            return Result.Ok<BillingInfo>(new BillingInfo(value));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
