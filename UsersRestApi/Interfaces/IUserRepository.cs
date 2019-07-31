﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        User Single(ISpecification<User> spec);

        bool IsUniqueEmail(Email email);

        bool Any(ISpecification<User> spec);
    }
}
