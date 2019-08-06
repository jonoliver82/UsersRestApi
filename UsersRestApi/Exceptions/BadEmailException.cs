// **********************************************************************************
// Filename					- BadEmailException.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;

namespace UsersRestApi.Exceptions
{
    public class BadEmailException : ValidationException
    {
        public BadEmailException(string email)
            : base("Bad email - " + email)
        {
        }

        public BadEmailException()
        {
        }

        public BadEmailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
