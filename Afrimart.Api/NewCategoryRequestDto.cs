using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.Api
{
    public class NewCategoryRequestDto
    {
        public int ParentCategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageUri { get; set; }
    }
}
