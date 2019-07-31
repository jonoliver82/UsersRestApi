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

        public UserFactory(IEmailUniquenessValidater validater)
        {
            _validater = validater;
        }

        public User Create(string name, Email email, Password password)
        {
            _validater.Validate(email);

            return new User(name, email, password);
        }
    }
}
