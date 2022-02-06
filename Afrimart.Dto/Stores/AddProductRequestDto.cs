using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afrimart.Dto.Stores
{
    public class AddProductRequestDto
    {
        [Required]
        [RegularExpression("^[-A-Za-z0-9 ,_'=.%]+$", ErrorMessage = "Only alphabets and a few specials allowed")]
        [StringLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(1200, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string SelectedCategory { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
        public bool IsOnSale { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }
        public decimal DisplayPrice { get; set; } // To be computed for display only

        public bool IsWeighted { get; set; }
        public string WeightUnit { get; set; }

        public decimal Tax { get; set; }
        public string Tags { get; set; }

        public int QuantityAvailable { get; set; }
        public decimal PlatformFee { get; set; }
    }
}
