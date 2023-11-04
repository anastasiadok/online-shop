using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.db.Models
{
    public partial class Subcategory
    {
        public Guid SubcategoryId { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual Category Category { get; set; } = null!;

    }
}
