using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Email FindUserEmailById(UserId id)
        {
            var user = _repository.Select(new UserSpecification(id));
            return user.Email;
        }
    }
}
