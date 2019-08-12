using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Domain;

namespace UsersRestApi.Interfaces
{
    public interface IPaymentGateway
    {
        Result ChargeCommission(BillingInfo billingInfo);

        void RollbackLastTransaction();
    }
}
