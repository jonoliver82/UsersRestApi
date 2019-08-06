// **********************************************************************************
// Filename					- UsersFinderService.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Queries;

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
