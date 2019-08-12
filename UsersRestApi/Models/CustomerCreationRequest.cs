using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRestApi.Models
{
    public class CustomerCreationRequest
    {
        public string Name { get; set; }

        public string BillingInfo { get; set; }
    }
}
