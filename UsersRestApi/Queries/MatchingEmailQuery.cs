using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Queries
{
    public class MatchingEmailQuery : BaseQuery<User>
    {
        public MatchingEmailQuery(Email email)
            : base(u => u.Email == email)
        {
        }
    }
}
