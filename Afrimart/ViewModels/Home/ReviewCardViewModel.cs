using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afrimart.ViewModels.Home
{
    public class ReviewCardViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerImageUri { get; set; }
        public string Review { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
