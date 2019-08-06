// **********************************************************************************
// Filename					- NotUniqueEmailAddressException.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;
using UsersRestApi.Domain;

namespace UsersRestApi.Exceptions
{
    public class NotUniqueEmailAddressException : ValidationException
    {
        public NotUniqueEmailAddressException(Email email)
            : base($"Not unique email address - {email}")
        {
        }

        public NotUniqueEmailAddressException()
        {
        }

        public NotUniqueEmailAddressException(string message)
            : base(message)
        {
        }

        public NotUniqueEmailAddressException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
