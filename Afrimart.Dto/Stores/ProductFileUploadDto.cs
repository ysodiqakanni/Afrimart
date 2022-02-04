using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Afrimart.Dto.Stores
{
    public class ProductFileUploadDto
    {
        [Required, Display(Name = "File Name")]
        public string FileName { get; set; } 
        public FileType FileType { get; set; }
        [Required, Display(Name = "File Url")]
        public string FileUri { get; set; }
    }
}
