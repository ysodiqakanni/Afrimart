using System;
using System.Collections.Generic;
using System.Text;

namespace Afrimart.Dto.Carts
{
    public class AddToCartRequestDto
    {
        public string CartId { get; set; }
        public string PSIN { get; set; }
        public int Count { get; set; }
    }
}
