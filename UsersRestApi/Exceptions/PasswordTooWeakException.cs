// **********************************************************************************
// Filename					- PasswordTooWeakException.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;

namespace UsersRestApi.Exceptions
{
    public class PasswordTooWeakException : ValidationException
    {
        public PasswordTooWeakException()
            : base("Password too weak")
        {
        }

        public PasswordTooWeakException(string message)
            : base(message)
        {
        }

        public PasswordTooWeakException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
