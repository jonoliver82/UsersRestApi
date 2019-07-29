using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Specifications
{
    // TODO create a query that requires a more advanced specification
    public class UserIdSpecification : BaseSpecification<User>
    {
        public UserIdSpecification(int id)
            : base(u => u.Id == id)
        {
        }
    }
}
