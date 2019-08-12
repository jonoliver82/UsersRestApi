using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UsersRestApi.Core;
using UsersRestApi.Domain;
using UsersRestApi.Interfaces;
using UsersRestApi.Models;

namespace UsersRestApi.Controllers
{
    /// <summary>
    /// See https://enterprisecraftsmanship.com/2015/03/20/functional-c-handling-failures-input-errors/
    /// And https://gist.github.com/vkhorikov/7852c7606f27c52bc288
    /// </summary>
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentGateway _paymentGateway;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public CustomersController(ICustomerRepository customerRepository,
            IPaymentGateway paymentGateway,
            IEmailSender emailSender,
            ILogger<CustomersController> logger)
        {
            _customerRepository = customerRepository;
            _paymentGateway = paymentGateway;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public HttpResponseMessage CreateCustomer([FromBody]CustomerCreationRequest request)
        {
            Result<CustomerName> customerNameResult = CustomerName.Create(request.Name);
            if (customerNameResult.Failure)
            {
                _logger.LogInformation(customerNameResult.Error);
                return Error(customerNameResult.Error);
            }

            Result<BillingInfo> billingInfoResult = BillingInfo.Create(request.BillingInfo);
            if (billingInfoResult.Failure)
            {
                _logger.LogInformation(billingInfoResult.Error);
                return Error(billingInfoResult.Error);
            }

            Result chargeResult = _paymentGateway.ChargeCommission(billingInfoResult.Value);
            if (chargeResult.Failure)
            {
                _logger.LogInformation(chargeResult.Error);
                return Error(chargeResult.Error);
            }

            Customer customer = new Customer(customerNameResult.Value);
            Result saveResult = _customerRepository.Save(customer);
            if (saveResult.Failure)
            {
                _paymentGateway.RollbackLastTransaction();
                _logger.LogInformation(saveResult.Error);
                return Error(saveResult.Error);
            }

            _emailSender.SendGreetings(customerNameResult.Value);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private static HttpResponseMessage Error(string value)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = value,
            };
        }
    }
}