using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserIdFactory
    {
        UserId Create();
    }
}
