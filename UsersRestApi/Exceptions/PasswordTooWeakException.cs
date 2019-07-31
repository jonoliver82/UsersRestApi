using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Exceptions
{
    public class PasswordTooWeakException : ValidationException
    {
        public PasswordTooWeakException()
            : base("Password too weak")
        {
        }
    }
}
