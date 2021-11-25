using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;
using Test.DTO.DTO;
using Test.Repo.Interface;
using Test.Service.Interface;

namespace Test.Service.Implementation
{
    public class CategoryService : IcategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        public CategoryService( ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public void CreateAsync(CategoryDTO Category)
        {
            try
            {

          
            var category = _mapper.Map<Test.Data.Models.Category>(Category);
            _categoryRepo.CreateAsync(category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _categoryRepo.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CategoryDTO Get(Guid id)
        {
            try
            {
              var cat=  _categoryRepo.Get(id);
                var dto= _mapper.Map<CategoryDTO>(cat);
                return dto;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public IEnumerable<CategoryDTO> Getall()
        {
            try
            {
                var cats = _categoryRepo.Getall();
                var dto = _mapper.Map<List<CategoryDTO>>(cats);
                return dto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<CategoryDTO> Search(string Name)
        {
            try
            {
                var cats = _categoryRepo.Search(Name);
                var dto = _mapper.Map<List<CategoryDTO>>(cats);
                return dto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void Update(Guid id, CategoryDTO category)
        {
            try
            {
                var dto = _mapper.Map<Category>(category);
                 _categoryRepo.Update(id,dto);
               
                
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
