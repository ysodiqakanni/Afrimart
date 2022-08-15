using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class OrderPaymentAudit: Entity
    {
        public decimal AmountPaid { get; set; }
        public decimal TransactionFee { get; set; } // removed from the amount the customer paid

        public string OrderIdentifier { get; set; }
        public string PaymentReference { get; set; }
        public string PayerEmail { get; set; }
    }
}
