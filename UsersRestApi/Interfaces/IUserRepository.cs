// **********************************************************************************
// Filename					- IUserRepository.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;
using System.Linq.Expressions;
using UsersRestApi.Core;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        Maybe<User> Read(IQuery<User> query);

        Maybe<User> Read(Expression<Func<User, bool>> predicate);

        bool Any(Expression<Func<User, bool>> predicate);

        bool Any(IQuery<User> query);
    }
}
