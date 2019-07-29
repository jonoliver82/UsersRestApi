using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        User Single(ISpecification<User> spec);
    }
}
