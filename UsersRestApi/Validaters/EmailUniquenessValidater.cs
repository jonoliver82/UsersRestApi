using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;
using UsersRestApi.Specifications;

namespace UsersRestApi.Validaters
{
    public class EmailUniquenessValidater : IEmailUniquenessValidater
    {
        private readonly IUserRepository _repository;

        public EmailUniquenessValidater(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Validate(Email email)
        {
            if (!_repository.Any(new MatchingEmailSpecification(email)))
            {
                throw new NotUniqueEmailAddress(email);
            }
        }
    }
}
