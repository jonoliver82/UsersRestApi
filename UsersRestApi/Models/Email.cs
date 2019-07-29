using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    /// <summary>
    /// A value object
    /// </summary>
    public class Email
    {
        private readonly string _email;

        public Email(string email)
        {
            _email = email;
        }
    }
}
