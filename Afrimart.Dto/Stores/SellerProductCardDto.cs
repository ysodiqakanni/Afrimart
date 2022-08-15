using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Stores
{ 
    public class SellerProductCardDto
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}
