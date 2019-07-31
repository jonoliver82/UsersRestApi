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

        // TODO not used
        // TODO not in interface
        public bool HasErrors()
        {
            return _errors.Any();
        }

        /// <summary>
        /// TODO not in interface
        /// Collect all the errors
        /// </summary>
        public IEnumerable<string> GetErrors()
        {
            // TODO check java documentation for stream - map - collect
            // java: errors.stream().map(Throwable::getMessage).collect(Collectors.toList());           
            return _errors.Select(x => x.Message);
        }
    }
}
