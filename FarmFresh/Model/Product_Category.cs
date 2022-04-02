using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmFresh.Model
{
    public class Product_Category
    {
        public long ProductID { get; set; }

        public long CategoryID { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }
    }
}
