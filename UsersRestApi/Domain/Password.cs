using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Domain
{
    /// <summary>
    /// A value object
    /// </summary>
    public class Password
    {
        public Password(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
