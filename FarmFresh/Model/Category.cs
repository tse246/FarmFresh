using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmFresh.Model
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public virtual ICollection<Product_Category> Product_Categories { get; set; }
    }
}
