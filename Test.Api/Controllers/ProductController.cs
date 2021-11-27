using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DTO.DTO;
using Test.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IcategoryService _catservice;
        public ProductController(IProductService service, IcategoryService categoryService)
        {

            _service = service;
            _catservice = categoryService;
        }
      
        // GET: api/<ValuesController>
        [Authorize(Roles = "Manager,Customer,Admin")]
        [HttpGet("allproducts")]
        public IEnumerable<ProductDTO> Get()
        {
            return _service.Getall();
        }
        [Authorize(Roles = "Manager,Customer,Admin")]
        [HttpGet("Search/{name}")]
        public IEnumerable<ProductDTO> Search(string name)
        {
            return _service.Search(name);
        } 
        [Authorize(Roles = "Manager,Customer,Admin")]
        [HttpGet("SearchDate")]
        public IEnumerable<ProductDTO> Searchdate([FromQuery]string date)
        {
           var dates= DateTime.Parse(date);
            return _service.Search(dates);
        }
        [Authorize(Roles = "Manager,Customer,Admin")]
        // GET api/<ValuesController>/5
        [HttpGet("get/{id}")]
        public ProductDTO Get(Guid id)
        {
            return _service.Get(id);
        }
        [Authorize(Roles = "Manager,Admin")]
        // POST api/<ValuesController>
        [HttpPost("Create")]
        public IActionResult Post([FromBody] ProductDTO value)
        {
            try
            {
                _service.Create(value);
                return Ok();
            }
            catch (Exception)
            {
                return NoContent();
                throw;
            }
            

        }
        [Authorize(Roles = "Manager,Admin")]
        // PUT api/<ValuesController>/5
        [HttpPut("update/{id}")]
        public IActionResult Put(Guid id, [FromBody] ProductDTO value)
        {
            try
            {
                _service.Update(id,value);
                return Ok();
            }
            catch (Exception)
            {
                return NoContent();
                throw;
            }
        }
        [Authorize(Roles = "Admin")]
        // DELETE api/<ValuesController>/5
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return NoContent();
                throw;
            }
        }
    }
}
