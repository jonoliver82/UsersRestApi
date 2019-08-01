using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;
using UsersRestApi.Specifications;

namespace UsersRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool Any(Expression<Func<User,bool>> predicate)
        {
            return _context.Set<User>().Any(predicate);
        }

        public bool Any(IQuery<User> query)
        {
            return _context.Set<User>().Any(query.Criteria);
        }

        public User SingleOrDefault(Expression<Func<User, bool>> predicate)
        {
            return _context.Set<User>().SingleOrDefault(predicate);
        }

        public User SingleOrDefault(IQuery<User> query)
        {
            return _context.Set<User>().SingleOrDefault(query.Criteria);
        }
    }
}
