using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class ShoppingCart:Entity
    {
        [Required]
        public string CartIdentifier { get; set; } 
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem:Entity
    {
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal NetAmount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
