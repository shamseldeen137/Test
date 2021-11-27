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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryService _service;
        public CategoryController(IcategoryService service)
        {
            _service = service;
        }
        // GET: api/<CategoryController>
        [Authorize(Roles = "Manager,Customer,Admin")]
 
        [HttpGet("all-categories")]
        public IEnumerable<CategoryDTO> Get()
        {

            return _service.Getall();
           
        }
        [Authorize(Roles = "Manager,Customer,Admin")]
        // GET api/<CategoryController>/5
        [HttpGet("get-category/{id}")]
        public CategoryDTO Get(Guid id)
        {
            return _service.Get(id);
        }   
        [Authorize(Roles = "Manager,Customer,Admin")]
        // GET api/<CategoryController>/5
        [HttpGet("search-category/{name}")]
        public IEnumerable< CategoryDTO> Search(string name)
        {
            return _service.Search(name);
        }
        [Authorize(Roles = "Manager,Admin")]
        // POST api/<CategoryController>
        [HttpPost("Create-Category")]
        public ActionResult Post([FromBody] CategoryDTO value)
        {
            try
            {
                _service.CreateAsync(value);
                return Ok();
            }
            catch (Exception)
            {
                return NoContent();
                throw;
            }
        }
        [Authorize(Roles = "Manager,Admin")]
        // PUT api/<CategoryController>/5
        [HttpPut("Edit-category/{id}")]
        public ActionResult Put(Guid id, [FromBody] CategoryDTO value)
        {
            try
            {
                _service.Update(id, value);
                return Ok();
            }
            catch (Exception)
            {
                return NoContent();
                throw;
            }
        }
        [Authorize(Roles = "Admin")]
        // DELETE api/<CategoryController>/5
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
