using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DTO.DTO;
using Test.Repo.Interface;
using Test.Service.Interface;

namespace Test.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        public void Create(ProductDTO Product)
        {
            try
            {


                var prod = _mapper.Map<Test.Data.Models.Product>(Product);
                _productRepo.Create(prod);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                _productRepo.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProductDTO Get(Guid id)
        {
            try
            {
                var cat = _productRepo.Get(id);
                var dto = _mapper.Map<ProductDTO>(cat);
                return dto;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public IEnumerable<ProductDTO> Getall()
        {
            try
            {
                var pros = _productRepo.Getall();
                var dto = _mapper.Map<List<ProductDTO>>(pros);
                return dto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<ProductDTO> Search(string Name = "", DateTime? date = null)
        {
            try
            {
                var cats = _productRepo.Search(Name,date);
                var dto = _mapper.Map<List<ProductDTO>>(cats);
                return dto;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void Update(Guid id, ProductDTO Product)
        {
            try
            {
                var dto = _mapper.Map<Test.Data.Models.Product>(Product);
                _productRepo.Update(id, dto);


            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
