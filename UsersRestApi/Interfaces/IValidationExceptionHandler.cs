using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Exceptions;

namespace UsersRestApi.Interfaces
{
    public interface IValidationExceptionHandler
    {
        void Add(ValidationException exception);
    }
}
