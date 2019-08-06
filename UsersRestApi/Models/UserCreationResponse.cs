// **********************************************************************************
// Filename					- UserCreationResponse.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System.Collections.Generic;
using System.Linq;

namespace UsersRestApi.Models
{
    public class UserCreationResponse
    {
        public int Id { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public static UserCreationResponse Success(int id)
        {
            var response = new UserCreationResponse
            {
                Id = id,
                Errors = Enumerable.Empty<string>(),
            };
            return response;
        }

        public static UserCreationResponse Failure(IEnumerable<string> errors)
        {
            var response = new UserCreationResponse
            {
                Errors = errors,
            };
            return response;
        }
    }
}
