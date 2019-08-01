using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public bool IsSatisfiedBy(Email email)
        {
            return !_userRepository.Any(new MatchingEmailQuery(email));
        }
    }
}
