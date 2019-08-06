// **********************************************************************************
// Filename					- UserFactory.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Specifications;

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

            validationExceptionHandler.Add(new NotUniqueEmailAddressException(email));
            return new Maybe<User>();
        }
    }
}
