using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Queries;
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

        public Maybe<Email> FindUserEmailById(int id)
        {
            var user = _repository.SingleOrDefault(new UserIdQuery(id));
            if (user != null)
            {
                return new Maybe<Email>(user.Email);
            }
            else
            {
                return new Maybe<Email>();
            }
        }
    }
}
