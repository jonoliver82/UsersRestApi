// **********************************************************************************
// Filename					- ValidationException.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;

namespace UsersRestApi.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException()
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
