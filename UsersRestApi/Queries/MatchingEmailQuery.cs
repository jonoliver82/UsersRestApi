// **********************************************************************************
// Filename					- MatchingEmailQuery.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Domain;
using UsersRestApi.Models;

namespace UsersRestApi.Queries
{
    public class MatchingEmailQuery : BaseQuery<User>
    {
        public MatchingEmailQuery(Email email)
            : base(u => u.Email == email)
        {
        }
    }
}
