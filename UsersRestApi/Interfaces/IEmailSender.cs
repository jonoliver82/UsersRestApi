using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Domain;

namespace UsersRestApi.Interfaces
{
    public interface IEmailSender
    {
        void SendGreetings(CustomerName name);
    }
}
