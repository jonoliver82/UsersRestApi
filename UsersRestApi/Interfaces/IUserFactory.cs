// **********************************************************************************
// Filename					- IUserFactory.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface IUserFactory
    {
        Maybe<User> Create(string name, Email email, Password password, IValidationExceptionHandler validationExceptionHandler);
    }
}
