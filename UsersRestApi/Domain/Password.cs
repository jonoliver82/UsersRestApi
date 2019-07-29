using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Domain
{
    /// <summary>
    /// A value object
    /// </summary>
    public class Password : ValueObject
    {
        public Password(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value;
        }
    }
}
