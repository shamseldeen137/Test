using System;
using System.Collections.Generic;

#nullable disable

namespace Test.DTO.DTO
{
    public  class CategoryDTO
    {
    

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? AddingDate { get; set; }

        public  List<ProductDTO> Products { get; set; }
    }
}
