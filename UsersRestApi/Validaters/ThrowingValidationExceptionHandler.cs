using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Validaters
{
    public class ThrowingValidationExceptionHandler : IValidationExceptionHandler
    {
        /// <summary>
        /// Immediately re-throws the exception
        /// </summary>        
        public void Add(ValidationException exception)
        {
            throw exception;
        }
    }
}
