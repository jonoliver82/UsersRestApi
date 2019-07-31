using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Specifications
{
    public class MatchingEmailSpecification : BaseSpecification<User>
    {
        public MatchingEmailSpecification(Email email)
            : base(u => u.Email == email)
        {
        }
    }
}
