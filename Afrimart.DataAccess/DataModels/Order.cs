using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class Order:Entity
    {
        public string Identifier { get; set; } // Unique Identifier: combine prodId(orAsin), categoryId, noOfItems,storeId, ddmmyyyy, rand4digits

        public string ShippedToPersonName { get; set; }
        public string ShippedToAddress { get; set; } // concatenate line1, line2, city, state, zip
        public OrderStatus Status { get; set; }
        public ShippingMethod ShippingMethod { get; set; } // Todo: Shipping should be handled as individual entities later

        public List<LineItem> LineItems { get; set; }
       
        // costs breakdown
        public decimal SubTotal { get; set; }  // initial total cost of all items
        public decimal ShippingCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }

        public decimal TotalAmountPaid { get; set; }
    }
    public class LineItem : Entity
    {
        public int Quantity { get; set; }
        public decimal SoldPricePerQuantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } 
    }

  
}
