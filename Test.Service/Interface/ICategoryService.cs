using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DTO.DTO;

namespace Test.Service.Interface
{
    public  interface IcategoryService
    {
        public IEnumerable<CategoryDTO> Getall();
        public CategoryDTO Get(Guid id);
        public IEnumerable<CategoryDTO> Search(string Name);
        public void CreateAsync(CategoryDTO Category);
        public void Delete(Guid id);
        public void Update(Guid id, CategoryDTO Category);
    }
}
