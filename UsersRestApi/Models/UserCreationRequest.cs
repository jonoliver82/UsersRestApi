// **********************************************************************************
// Filename					- UserCreationRequest.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

namespace UsersRestApi.Models
{
    public class UserCreationRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
