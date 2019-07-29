using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    /// <summary>
    /// A value object
    /// </summary>
    public class Password
    {
        private readonly string _value;

        public Password(string value)
        {
            _value = value;
        }
    }
}
