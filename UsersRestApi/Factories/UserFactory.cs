using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Queries;
using UsersRestApi.Specifications;
using UsersRestApi.Validaters;

namespace UsersRestApi.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserRepository _userRepository;

        public UserFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Maybe<User> Create(string name, Email email, Password password, IValidationExceptionHandler validationExceptionHandler)
        {
            var rule = new IsEmailUniqueSpecification(_userRepository);
            if (rule.IsSatisfiedBy(email))
            {
                return new Maybe<User>(new User(name, email, password));
            }

            validationExceptionHandler.Add(new NotUniqueEmailAddress(email));
            return new Maybe<User>();
        }
    }
}
