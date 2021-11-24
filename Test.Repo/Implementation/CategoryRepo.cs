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
    class CategoryRepo : ICategoryRepo
    {
        private readonly DBContext _dBContext;
        private readonly DbSet<Category> Categories;

        public CategoryRepo(DBContext dBContext)
        {
          _dBContext=  dBContext;
            Categories = _dBContext.Categories;
        }
        public async void CreateAsync(Category Category)
        {
           await Categories.AddAsync(Category);
            SaveChanges();
        }

        public  void Delete(Guid id)
        {
             Categories.Remove(Categories.FirstOrDefault(Categories=>Categories.Id==id));
            SaveChanges();
          
        }

        public Category Get(Guid id)
        {
            return Categories.FirstOrDefault(cat => cat.Id == id);
        }

        public IEnumerable<Category> Getall()
        {
            return Categories;
        }

        public IEnumerable<Category> Search(string Name)
        {
            return Categories.Where(cat => cat.Name.ToLower().Contains(Name.ToLower().Trim()));
        }

        public void Update(Guid id, Category Category)
        {
            throw new NotImplementedException();
        }
        void SaveChanges()
        {
            _dBContext.SaveChanges();
        }
    }
}
