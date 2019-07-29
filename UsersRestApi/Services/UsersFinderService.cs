using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Specifications;

namespace UsersRestApi.Services
{
    public class UsersFinderService : IUsersFinderService
    {
        private readonly IUserRepository _repository;

        public UsersFinderService(IUserRepository repository)
        {
            _repository = repository;
        }

        // TODO return a Maybe<Email>
        public Email FindUserEmailById(int id)
        {
            // Simple specification object
            var user = _repository.Single(new UserIdSpecification(id));
            return user?.Email ?? new Email(string.Empty);
        }
    }
}
