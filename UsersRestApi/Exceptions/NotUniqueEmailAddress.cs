using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;

namespace UsersRestApi.Exceptions
{
    public class NotUniqueEmailAddress : ValidationException
    {
        public NotUniqueEmailAddress(Email email)
            : base($"Not unique email address - {email}")
        {
        }
    }
}
