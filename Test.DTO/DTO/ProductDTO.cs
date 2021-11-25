using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Test.DTO.DTO
{
    public  class ProductDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? AddingDate { get; set; }
        [Display(Name="Category")]
        public Guid? CatId { get; set; }

       
    }
}
