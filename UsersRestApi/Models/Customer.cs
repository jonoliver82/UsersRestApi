using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Models
{
    public class Customer : IIdentifiable
    {
        public Customer(CustomerName name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public CustomerName Name { get; private set; }
    }
}
