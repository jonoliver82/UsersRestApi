using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        {
            // TODO validater could result in an exception thrown from the constructor
            _validater.Validate(address);

            Address = address;
        }

        public string Address { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Address;
        }
    }
}
