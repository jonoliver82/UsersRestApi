using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    public class User
    {
        private readonly UserId _id;
        private readonly Email _email;
        private readonly Password _password;

        public User(UserId id, Email email, Password password)
        {
            _id = id;
            _email = email;
            _password = password;
        }

        public UserId Id => _id;

        public Email Email => _email;

        public Password Password => _password;
    }
}
