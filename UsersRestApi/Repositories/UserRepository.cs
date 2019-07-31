using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Any(ISpecification<User> spec)
        {
            return _context.Set<User>().Any(spec.Criteria);
        }

        public bool IsUniqueEmail(Email email)
        {
            var spec = new MatchingEmailSpecification(email);
            return Any(spec);

        }

        public User Single(ISpecification<User> spec)
        {
            return _context.Set<User>().SingleOrDefault(spec.Criteria);
        }
    }
}
