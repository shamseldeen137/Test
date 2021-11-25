using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
             var result=_service.Getall();
            return View(result);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(Guid id)
        {
            var prod = _service.Get(id);
            return View(prod);
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
        public ActionResult Create(ProductDTO dto)
        {
            try
            {
                _service.Create(dto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
