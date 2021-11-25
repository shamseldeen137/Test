using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Data.Models;
using Test.DTO.DTO;

namespace Test.Service
{
   public class MapperProfile: Profile
    {


      
            public MapperProfile()
            {
                CreateMap<Product, ProductDTO>().ReverseMap();
                CreateMap<Category, CategoryDTO>().ReverseMap();
            }
        
    }
}
