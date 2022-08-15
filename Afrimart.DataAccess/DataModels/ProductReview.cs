using System;
using System.Collections.Generic;
using System.Text;
using DataModels;

namespace Afrimart.DataAccess.DataModels
{
    public class ProductReview: Entity
    {
        public string Review { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public int Rating { get; set; }

        public string ReviewerName { get; set; }
        public string ReviewerImageUri { get; set; }

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

        // https://stackoverflow.com/a/17127512
        public int? ReviewerId { get; set; }
        public User Reviewer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
