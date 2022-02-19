using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto.Orders;
using Afrimart.Service.Contracts;

namespace Afrimart.Service.Implementations
{
    public class StripeService: IPaymentService
    {
        public bool ChargePayment(CardDetailsDto cardDetails, string customerEmail, string orderIdentifier)
        {
            return true;
        }
    }
}
