using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    public class UserCreationResponse
    {
        public int Id { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public static UserCreationResponse Success(int id)
        {
            var response = new UserCreationResponse
            {
                Id = id,
                Errors = Enumerable.Empty<string>(),
            };
            return response;
        }

        public static UserCreationResponse Failure(IEnumerable<string> errors)
        {
            var response = new UserCreationResponse
            {
                Errors = errors,
            };
            return response;
        }
    }
}
