using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Afrimart.ViewModels.Sellers
{
    public class AddProductViewModel
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

        public string UPC { get; set; }  // Do all products have this? or we should make it optional

        [Required(ErrorMessage = "You must upload an image")] 
        [DataType(DataType.Upload)]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })] 
        public IFormFile DisplayImage { get; set; }

        [MaxFileSize(3 * 1024 * 1024)]
        [AllowedExtensions(new string[] {".jpg", ".jpeg", ".png"})]
        public List<IFormFile> GalleryImages { get; set; } = new List<IFormFile>();

        public DashboardHeaderViewModel DashboardHeaderViewModel { get; set; } = new DashboardHeaderViewModel();
        public List<SelectListItem> ProductCategories { get; set; }
    }
}
