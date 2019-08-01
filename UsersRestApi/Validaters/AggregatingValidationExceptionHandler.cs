using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Exceptions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Validaters
{
    public class AggregatingValidationExceptionHandler : IValidationExceptionHandler
    {
        private List<ValidationException> _errors;

        public AggregatingValidationExceptionHandler()
        {
            _errors = new List<ValidationException>();
        }

        public void Add(ValidationException exception)
        {
            _errors.Add(exception);
        }

        public bool HasErrors => _errors.Any();

        public IEnumerable<string> Errors => _errors.Select(x => x.Message);
        
    }
}
