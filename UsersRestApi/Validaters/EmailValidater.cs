using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Exceptions;
using System.Text.RegularExpressions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Validaters
{
    public class EmailValidater
    {
        private const string _pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$";
        
        public void Validate(string email, IValidationExceptionHandler validationExceptionHandler)
        {
            var rx = new Regex(_pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(email) || !rx.IsMatch(email))
            {
                validationExceptionHandler.Add(new BadEmailException(email));
            }
        }

    }
}
