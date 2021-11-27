using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test.DTO.DTO;
using Test.Service.Interface;

namespace Test.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IcategoryService _catservice;
        public ProductController(IProductService service,IcategoryService categoryService)
        {
          
            _service = service;
            _catservice = categoryService;
        }
        // GET: ProductController
        

        public async Task<ActionResult> IndexAsync()
        {
            IEnumerable<ProductDTO> result;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/product/allproducts"))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    result = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(apiResponse);
                }

            }
            return View(result);

         //   var result=_service.Getall();
           
           // return View(result);
        }
          public async Task<ActionResult> SearchNameAsync(String name)
        {

            IEnumerable<ProductDTO> result;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/product/search/" +name))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    result = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(apiResponse);
                }

            }
            //return View(result);
           // var result=_service.Search(name);
            ViewBag.searchname = name;
            return View("Index",result);
        }
          public async Task<ActionResult> SearchDateAsync(DateTime date)
        {
            IEnumerable<ProductDTO> result;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/product/SearchDate?date=" + date.ToString()))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    result = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(apiResponse);
                }

            }
           
            ViewBag.searchdate = date;
            return View("Index", result);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> DetailsAsync(Guid id)
        {


            ProductDTO categories;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/product/get/" + id))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    categories = JsonConvert.DeserializeObject<ProductDTO>(apiResponse);
                }

            }
            return View(categories);

        
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
          ViewBag.Category= new SelectList(  _catservice.Getall(), "Id","Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProductDTO dto)
        {

            try
            {
                try
                {
                    dynamic categories;
                    using (var httpClient = new HttpClient())
                    {
                        string token = "";
                        token = HttpContext.Session.GetString("JWToken");

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");


                        var myContent = JsonConvert.SerializeObject(dto);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        using (var response = await httpClient.PostAsync("https://localhost:5001/api/product/create/", byteContent))
                        {

                            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                return View("Unauthorized");
                            }
                            string apiResponse = await response.Content.ReadAsStringAsync();




                            categories = JsonConvert.DeserializeObject<IActionResult>(apiResponse);
                        }

                    }
                }
                catch (Exception)
                {

                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }



        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> EditAsync(Guid id)
        {
            ProductDTO categories;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/product/get/" + id))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    categories = JsonConvert.DeserializeObject<ProductDTO>(apiResponse);
                }

            }
           
            ViewBag.Category = new SelectList(_catservice.Getall(), "Id", "Name", categories.CatId);
            return View(categories);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid id, ProductDTO collection)
        {


            try
            {
                try
                {
                    dynamic categories;
                    using (var httpClient = new HttpClient())
                    {
                        string token = "";
                        token = HttpContext.Session.GetString("JWToken");

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");


                        var myContent = JsonConvert.SerializeObject(collection);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        using (var response = await httpClient.PostAsync("https://localhost:5001/api/category/Edit-product/" + id, byteContent))
                        {

                            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                return View("Unauthorized");
                            }
                            string apiResponse = await response.Content.ReadAsStringAsync();




                            categories = JsonConvert.DeserializeObject<IActionResult>(apiResponse);
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }





        }


        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            ProductDTO categories;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/product/get/" + id))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    categories = JsonConvert.DeserializeObject<ProductDTO>(apiResponse);
                }

            }
            return View(categories);
           
        }  
    

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Guid id, IFormCollection collection)
        {

            ProductDTO categories;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/product/delete/" + id))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                   
                }

            }
            return RedirectToAction(nameof(IndexAsync));



          
        }
    }
}
