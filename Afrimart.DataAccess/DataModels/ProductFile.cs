using System;
using System.Collections.Generic;
using System.Text;
using Afrimart.Dto;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    /// <summary>
    /// To save gallery images, videos and any other file
    /// </summary>
    public class ProductFile: Entity
    {
        public string FileName { get; set; }
        public string FileUri { get; set; }
        public FileType FileType { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    } 
}
