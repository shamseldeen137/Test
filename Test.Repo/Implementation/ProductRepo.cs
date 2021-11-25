using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;
using Test.Repo.Interface;

namespace Test.Repo.Implementation
{
  public  class ProductRepo : IProductRepo
    {
        private readonly DBContext _dBContext;
        private readonly DbSet<Product> Products;

        public ProductRepo(DBContext dBContext)
        {
            _dBContext = dBContext;
            Products = _dBContext.Products;
        }
        public void Create(Product Product)
        {
            Products.Add(Product);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            Products.Remove(Products.FirstOrDefault(Prod => Prod.Id == id));
            SaveChanges();

        }

        public Product Get(Guid id)
        {
            return Products.FirstOrDefault(Prod => Prod.Id == id);
        }

        public IEnumerable<Product> Getall()
        {
            return Products;
        }

        public IEnumerable<Product> Search(string Name = "")
        {
            var result = Products.Where(a => a.Name.ToLower().Contains(Name.Trim().ToLower()) ).ToList();



                return result ;
                } 
        public IEnumerable<Product> Search( DateTime? date = null)
        {
            var result = Products.Where(a => a.AddingDate.Value.Date == date.Value.Date).ToList();



                return result ;
                }

        public void Update(Guid id, Product Product)
        {
            Products.Update(Product);
            SaveChanges();
        }
        void SaveChanges()
        {
            _dBContext.SaveChanges();
        }
    }
}
