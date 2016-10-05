using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PersonalTrainerDiet.Controllers
{
    public class DietController : Controller
    {
        private readonly IProductManagement productManagement;


        public DietController(IProductManagement productManagement)
        {
            this.productManagement = productManagement;
        }

        public IActionResult Diet()
        {
            return View();
        }


        public IActionResult Day()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new ProductDto());
        }

        [HttpPost]
        public IActionResult EditProduct(Guid productId)
        {
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult DeleteProduct(Guid productId)
        {
            productManagement.RemoveProduct(productId);
            return RedirectToAction("ProductList", "Diet");
        }

        
        /// <summary>
        /// Odpowiedzialny za wyświetlanie listy produktów.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductList()
        {
            var products = productManagement.GetProducts();
            return View(products);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto dto)
        {
            productManagement.AddProduct(dto);

            return View(new ProductDto());
        }
    }
}
