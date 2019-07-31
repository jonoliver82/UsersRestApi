using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Exceptions;
using System.Text.RegularExpressions;

namespace UsersRestApi.Validaters
{
    public class EmailValidater
    {
        private const string _pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$";
        
        public void Validate(string email)
        {
            var rx = new Regex(_pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(email) || !rx.IsMatch(email))
            {
                throw new BadEmailException(email);
            }
        }

    }
}
