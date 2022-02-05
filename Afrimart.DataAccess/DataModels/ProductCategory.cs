using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class ProductCategory: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayImageUri { get; set; }

        public int? ParentId { get; set; }
        public ProductCategory Parent { get; set; }

        public List<ProductCategory> Children { get; set; }
    }
}
