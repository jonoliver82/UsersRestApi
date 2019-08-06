// **********************************************************************************
// Filename					- UserRepository.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;
using System.Linq;
using System.Linq.Expressions;
using UsersRestApi.Core;
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

        public bool Any(Expression<Func<User, bool>> predicate)
        {
            return _context.Set<User>().Any(predicate);
        }

        public bool Any(IQuery<User> query)
        {
            return Any(query.Criteria);
        }

        public Maybe<User> Read(Expression<Func<User, bool>> predicate)
        {
            var user = _context.Set<User>().SingleOrDefault(predicate);
            if (user != null)
            {
                return new Maybe<User>(user);
            }
            else
            {
                return new Maybe<User>();
            }
        }

        public Maybe<User> Read(IQuery<User> query)
        {
            return Read(query.Criteria);
        }
    }
}
