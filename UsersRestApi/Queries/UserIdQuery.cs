// **********************************************************************************
// Filename					- UserIdQuery.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using UsersRestApi.Models;

namespace UsersRestApi.Queries
{
    public class UserIdQuery : BaseQuery<User>
    {
        public UserIdQuery(int id)
            : base(u => u.Id == id)
        {
        }
    }
}
