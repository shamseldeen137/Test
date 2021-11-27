using Microsoft.AspNetCore.Authorization;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test.DTO.DTO;
using Test.Service.Interface;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Cors;
namespace Test.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly IcategoryService _service;
        public CategoryController(IcategoryService service)
        {
            _service = service;
        }
        // GET: CategoryController
       [HttpGet]
        public async Task<ActionResult> Index()
        {
           
            IEnumerable<CategoryDTO> categories;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");
           
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/category/all-categories"))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                     categories = JsonConvert.DeserializeObject<IEnumerable< CategoryDTO>>(apiResponse);
                }

            }
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> DetailsAsync(Guid id)
        {
            CategoryDTO categories;
            using (var httpClient = new HttpClient())
            {
                string token = "";
                token = HttpContext.Session.GetString("JWToken");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/category/get-category/"+id))
                {

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return View("Unauthorized");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();




                    categories = JsonConvert.DeserializeObject<CategoryDTO>(apiResponse);
                }

            }
            return View(categories);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(CategoryDTO cat)
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


                        var myContent = JsonConvert.SerializeObject(cat);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        using (var response = await httpClient.PostAsync("https://localhost:5001/api/category/create-category/", byteContent))
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

        // GET: CategoryController/Edit/5
        public ActionResult Edit(Guid id)
        {

            return View(_service.Get(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid id, CategoryDTO cat)
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


                        var myContent = JsonConvert.SerializeObject(cat);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        using (var response = await httpClient.PostAsync("https://localhost:5001/api/category/Edit-category/"+id, byteContent))
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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string token = "";
                    token = HttpContext.Session.GetString("JWToken");

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");
                    using (var response = await httpClient.GetAsync("https://localhost:5001/api/delete"+id))
                    {

                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            return View("Unauthorized");
                        }
                        string apiResponse = await response.Content.ReadAsStringAsync();




                       // categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(apiResponse);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
