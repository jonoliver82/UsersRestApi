using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;

namespace UsersRestApi.Factories
{
    public class GuidBasedUserIdFactory : IUserIdFactory
    {
        public UserId Create()
        {
            return UserId.Of(Guid.NewGuid().ToString());
        }
    }
}
