using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Afrimart.ViewModels.Sellers
{
    public class AddProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public bool IsOnSale { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DisplayPrice { get; set; } // To be computed for display only

        public string Tags { get; set; }

        [Required(ErrorMessage = "You must upload an image")] 
        [DataType(DataType.Upload)]
        [MaxFileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })] 
        public IFormFile DisplayImage { get; set; }

        [MaxFileSize(3 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public List<IFormFile> GalleryImages { get; set; }

        public DashboardHeaderViewModel DashboardHeaderViewModel { get; set; } = new DashboardHeaderViewModel();
    }
}
