using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;

namespace UsersRestApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SampleContext _context;

        public CustomerRepository(SampleContext context)
        {
            _context = context;
        }

        public Result Save(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Result.Fail("test");
        }
    }
}
