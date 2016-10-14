using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public IActionResult AddProduct(ProductDto dto)
        {
            productManagement.AddProduct(dto);
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult EditProduct2(ProductDto product)
        {
            productManagement.UpdateProduct(product);
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult EditProduct(Guid productId)
        {
            var product = productManagement.GetProduct(productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(Guid productId)
        {
            productManagement.RemoveProduct(productId);
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult SubscribeProduct(Guid productId)
        {
            productManagement.SubscribeProduct(productId);
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult CancelSubscription(Guid productId)
        {
            productManagement.CancelSubscription(productId);
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult ShowDetails(Guid productId)
        {
            //TODO
            // productManagement.CancelSubscription(productId);
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
            var dto = new ProductListDto()
            {
                ProductList = products,
                SelectedProduct = null
            };
            return View(dto);
        }

        [HttpPost]
        public JsonResult GetProductDetails([FromBody]JToken jsonBody)
        {
            var id = jsonBody.Value<String>("Id");
            var dto = new ProductListDto();
            dto.ProductList = productManagement.GetProducts();
            var productDetails = productManagement.GetProduct(new Guid(id));
            return new JsonResult(productDetails);
        }
    }
}
