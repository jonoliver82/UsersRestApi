// **********************************************************************************
// Filename					- IValidationExceptionHandler.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System.Collections.Generic;
using UsersRestApi.Exceptions;

namespace UsersRestApi.Interfaces
{
    public interface IValidationExceptionHandler
    {
        bool HasErrors { get; }

        IEnumerable<string> Errors { get; }

        void Add(ValidationException exception);
    }
}
