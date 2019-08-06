// **********************************************************************************
// Filename					- IsEmailUniqueSpecification.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Queries;

namespace UsersRestApi.Specifications
{
    public class IsEmailUniqueSpecification : ISpecification<Email>
    {
        private readonly IUserRepository _userRepository;

        public IsEmailUniqueSpecification(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsSatisfiedBy(Email entity)
        {
            return !_userRepository.Any(new MatchingEmailQuery(entity));
        }
    }
}
