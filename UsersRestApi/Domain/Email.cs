// **********************************************************************************
// Filename					- Email.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System.Collections.Generic;
using System.Text.RegularExpressions;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Domain
{
    /// <summary>
    /// A value object.
    /// </summary>
    public class Email : ValueObject
    {
        private const string _pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$";

        public Email(string address)
        {
            if (!IsValidAddress(address))
            {
                throw new BadEmailException(address);
            }

            Address = address;
        }

        public string Address { get; private set; }

        // TODO Accept should take a validator, and pass this instance to it...
        public static void Accept(string address, IValidationExceptionHandler validationExceptionHandler)
        {
            if (!IsValidAddress(address))
            {
                validationExceptionHandler.Add(new BadEmailException(address));
            }
        }

        public override string ToString()
        {
            return Address;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address;
        }

        private static bool IsValidAddress(string address)
        {
            var rx = new Regex(_pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return !string.IsNullOrEmpty(address) && rx.IsMatch(address);
        }
    }
}
