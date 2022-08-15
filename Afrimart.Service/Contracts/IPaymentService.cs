using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto.Orders;

namespace Afrimart.Service.Contracts
{
    public interface IPaymentService
    {
        bool ChargePayment(CardDetailsDto cardDetails, string customerEmail, string orderIdentifier);
    }
}
