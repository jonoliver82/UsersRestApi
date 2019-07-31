﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Exceptions;

namespace UsersRestApi.Validaters
{
    public class PasswordValidater
    {
        // TODO maxLength 5 should be provided by injection of an options object
        private const int MAX_LENGTH = 5;
        
        public void Validate(string value)
        {
            if (value == null || value.Length < MAX_LENGTH)
            {
                throw new PasswordTooWeakException();
            }
        }
    }
}
