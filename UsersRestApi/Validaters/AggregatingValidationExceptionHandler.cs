// **********************************************************************************
// Filename					- AggregatingValidationExceptionHandler.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System.Collections.Generic;
using System.Linq;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Validaters
{
    public class AggregatingValidationExceptionHandler : IValidationExceptionHandler
    {
        private readonly List<ValidationException> _errors;

        public AggregatingValidationExceptionHandler()
        {
            _errors = new List<ValidationException>();
        }

        public bool HasErrors => _errors.Any();

        public IEnumerable<string> Errors => _errors.Select(x => x.Message);

        public void Add(ValidationException exception)
        {
            _errors.Add(exception);
        }
    }
}
