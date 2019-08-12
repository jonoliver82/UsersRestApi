using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Services
{
    public class PaymentGatewayService : IPaymentGateway
    {
        public Result ChargeCommission(BillingInfo billingInfo)
        {
            return Result.Ok();
        }

        public void RollbackLastTransaction()
        {
            // TODO
        }
    }
}
