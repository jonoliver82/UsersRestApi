using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        User SingleOrDefault(IQuery<User> query);

        User SingleOrDefault(Expression<Func<User, bool>> predicate);

        bool Any(Expression<Func<User, bool>> predicate);

        bool Any(IQuery<User> query);
    }
}
