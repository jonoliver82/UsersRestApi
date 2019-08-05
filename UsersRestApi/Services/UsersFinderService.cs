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
            // Use the Composable overload rather than needing to explicitly define funcs ie
            // var maybeUser = _repository.SingleOrDefault(new UserIdQuery(id));
            // return maybeUser.Select(empty: () => new Maybe<Email>(),
            //      present: (user) => new Maybe<Email>(user.Email));
            return _repository.Read(new UserIdQuery(id)).Select(a => a.Email);
        }
    }
}
