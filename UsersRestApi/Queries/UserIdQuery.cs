using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Queries
{
    public class UserIdQuery : BaseQuery<User>
    {
        public UserIdQuery(int id)
            : base(u => u.Id == id)
        {
        }
    }
}
