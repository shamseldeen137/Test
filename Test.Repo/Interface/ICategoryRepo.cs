using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;
using Test.DTO.DTO;

namespace Test.Repo.Interface
{
  public  interface ICategoryRepo
    {
       public IEnumerable<Category> Getall();
       public Category Get(Guid id);
       public IEnumerable<Category> Search(string Name);
       public void CreateAsync(Category Category);
       public void Delete(Guid id);
       public void Update(Guid id, Category Category);

    }
}
