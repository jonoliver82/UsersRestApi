using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Exceptions
{
    public class BadEmailException : ValidationException
    {
        public BadEmailException(string email)
            : base("Bad email - " + email)
        {
        }
    }
}
