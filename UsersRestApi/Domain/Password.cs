// **********************************************************************************
// Filename					- Password.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System.Collections.Generic;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Domain
{
    /// <summary>
    /// A value object.
    /// </summary>
    public class Password : ValueObject
    {
        private const int MIN_LENGTH = 5;

        public Password(string value)
        {
            if (!IsValidValue(value))
            {
                throw new PasswordTooWeakException();
            }

            Value = value;
        }

        public string Value { get; private set; }

        // TODO Accept should take a validator, and pass this instance to it...
        public static void Accept(string value, IValidationExceptionHandler validationExceptionHandler)
        {
            if (!IsValidValue(value))
            {
                validationExceptionHandler.Add(new PasswordTooWeakException());
            }
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        private static bool IsValidValue(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= MIN_LENGTH;
        }
    }
}
