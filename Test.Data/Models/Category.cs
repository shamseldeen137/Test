using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? AddingDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
