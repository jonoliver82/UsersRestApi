// **********************************************************************************
// Filename					- IUsersFinderService.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Core;
using UsersRestApi.Domain;

namespace UsersRestApi.Interfaces
{
    public interface IUsersFinderService
    {
        Maybe<Email> FindUserEmailById(int id);
    }
}
