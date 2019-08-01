using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Models
{
    /// <summary>
    /// Entity owns properties of value object types
    /// </summary>
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

        [Required]
        public string Name { get; set; }

        // Note validation of value object types are performed in their constructors
        public Email Email { get; private set; }

        public Password Password { get; private set; }
    }
}
