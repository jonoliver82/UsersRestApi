using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    /// <summary>
    /// A value object
    /// </summary>
    public class UserId
    {
        private readonly string _id;

        public UserId(string id)
        {
            _id = id;
        }

        public static UserId Of(string id)
        {
            return new UserId(id);
        }
    }
}
