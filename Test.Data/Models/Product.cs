using System;
using System.Collections.Generic;

#nullable disable

namespace Test.Data.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? AddingDate { get; set; }
        public Guid? CatId { get; set; }

        public virtual Category Cat { get; set; }
    }
}
