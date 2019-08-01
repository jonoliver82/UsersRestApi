using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserFactory
    {
        Maybe<User> Create(string name, Email email, Password password, IValidationExceptionHandler validationExceptionHandler);
    }
}
