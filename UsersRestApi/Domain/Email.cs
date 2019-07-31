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
    public class Email : ValueObject
    {
        // TODO should value objects have validaters provided by dependency injection 
        // into their constructors? A factory would be better
        private readonly EmailValidater _validater = new EmailValidater();

        public Email(string address)
            : this(address, new ThrowingValidationExceptionHandler())
        {
        }

        public static Email Of(string address)
        {
            return new Email(address);
        }

        public Email(string address, IValidationExceptionHandler validationExceptionHandler)
        {
            // TODO validater could result in an exception thrown from the constructor
            _validater.Validate(address, validationExceptionHandler);

            Address = address;
        }

        public static void Test(string address, IValidationExceptionHandler validationExceptionHandler)
        {
            new EmailValidater().Validate(address, validationExceptionHandler);
        }

        public string Address { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Address;
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
