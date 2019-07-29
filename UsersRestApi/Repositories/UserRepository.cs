using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;

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

        public User Select(ISpecification<User> spec)
        {
            return _context.Set<User>().SingleOrDefault(spec.Criteria);
        }
    }
}
