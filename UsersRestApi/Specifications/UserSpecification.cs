using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(UserId userId)
            : base(u => u.Id == userId)
        {
        }
    }
}
