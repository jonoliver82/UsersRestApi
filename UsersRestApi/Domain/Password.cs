using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Interfaces;
using UsersRestApi.Validaters;

namespace UsersRestApi.Domain
{
    /// <summary>
    /// A value object
    /// </summary>
    public class Password : ValueObject
    {
        // TODO should value objects have validaters provided by dependency injection 
        // into their constructors? A factory would be better
        private readonly PasswordValidater _validater = new PasswordValidater();

        public Password(string value)
            : this(value, new ThrowingValidationExceptionHandler())
        {
        }

        public Password(string value, IValidationExceptionHandler validationExceptionHandler)
        {
            // TODO validater could result in an exception thrown from the constructor
            _validater.Validate(value, validationExceptionHandler);

            Value = value;
        }

        public static Password Of(string value)
        {
            return new Password(value);
        }

        public string Value { get; private set; }

        public static void Test(string value, IValidationExceptionHandler validationExceptionHandler)
        {
            new PasswordValidater().Validate(value, validationExceptionHandler);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
