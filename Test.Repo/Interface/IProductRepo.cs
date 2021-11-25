using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;

namespace Test.Repo.Interface
{
 public   interface IProductRepo
    {
        public IEnumerable<Product> Getall();
        public Product Get(Guid id);
        public void Create(Product Product);
        public void Delete(Guid id);
        public void Update(Guid id, Product Product); 
        public IEnumerable<Product> Search(string Name="",DateTime? date=null);
    }
}
