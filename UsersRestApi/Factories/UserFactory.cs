using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Validaters;

namespace UsersRestApi.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IEmailUniquenessValidater _validater;
        private readonly IValidationExceptionHandler _handler;

        public UserFactory(IEmailUniquenessValidater validater, IValidationExceptionHandler handler)
        {
            _validater = validater;
            _handler = handler;
        }

        public User Create(string name, Email email, Password password)
        {
            _validater.Validate(email, _handler);

            return new User(name, email, password);
        }
    }
}
