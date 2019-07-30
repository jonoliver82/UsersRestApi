using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Models
{
    public class User : IIdentifiable
    {
        public User()
        {
        }

        public User(string name, Email email, Password password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Email Email { get; private set; }

        public Password Password { get; private set; }
    }
}
