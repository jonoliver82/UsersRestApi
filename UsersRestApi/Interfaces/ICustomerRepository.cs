using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Models;

namespace UsersRestApi.Interfaces
{
    public interface ICustomerRepository
    {
        Result Save(Customer customer);
    }
}
