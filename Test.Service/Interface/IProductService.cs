using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DTO.DTO;

namespace Test.Service.Interface
{
    public interface IProductService
    {
        public IEnumerable<ProductDTO> Getall();
        public ProductDTO Get(Guid id);
        public void Create(ProductDTO Product);
        public void Delete(Guid id);
        public void Update(Guid id, ProductDTO Product);
        public IEnumerable<ProductDTO> Search(string Name = "", DateTime? date = null);
    }
}
