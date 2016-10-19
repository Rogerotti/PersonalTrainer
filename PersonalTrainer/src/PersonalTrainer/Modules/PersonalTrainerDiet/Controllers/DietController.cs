using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PersonalTrainerDiet.Controllers
{
    public class DietController : Controller
    {
        private readonly IProductManagement productManagement;

        private const String additionalMealsId = nameof(additionalMealsId);
        private const String productGuidId = nameof(productGuidId);
        private const String productQuantityId = nameof(productQuantityId);
        private const String productMealTypeId = nameof(productMealTypeId);
        private const String mealTypeId = nameof(mealTypeId);

        public DietController(IProductManagement productManagement)
        {
            this.productManagement = productManagement;
        }

        public IActionResult Diet()
        {
            return View();
        }

        //TODO
        //1. Framework serwisy do dodawania posiłków do dnia oraz do typu posiłku.
        //2. Wyszukiwanie produktów z baz danych.
        //3. ustandaryzowanie dietCOntrollera
        [HttpGet]
        public IActionResult Day()
        {
            DailyFoodDto ProductDto = null;

            var additionalMeal = TempData[additionalMealsId] as Boolean?;
            if (additionalMeal == null ? false : (Boolean)additionalMeal)
            {
               var guids = TempData[productGuidId] as IEnumerable<Guid>;
               var quants =  TempData[productQuantityId] as IEnumerable<Int32>;
               var mealType =  TempData[productMealTypeId] as IEnumerable<Int32>;
               var enumMealType = TempData[mealTypeId]  as Int32?;

                List<DailyFoodProductDto> lista = new List<DailyFoodProductDto>();
                var a = guids.ToArray();
                var b = quants.ToArray();
                var c = mealType.ToArray();
                for (int i = 0; i < a.Length; i++)
                {
                    lista.Add(new DailyFoodProductDto()
                    {
                        ProductId = a[i],
                        ProductQuantity = b[i],
                        MealType = (MealType)c[i]
                    });
                }

                ProductDto = productManagement.GetDailyFoodFromDailyFoodProductDto(DateTime.Today, lista);
            }

            if (ProductDto == null)
                ProductDto = productManagement.GetDailyFood(DateTime.Today);

            return View(ProductDto);
        }

        [HttpPost]
        public IActionResult Day(IEnumerable<Guid> productId, IEnumerable<Int32> quantity, IEnumerable<Int32> productMealType, Int32 buttonType)
        {
            TempData[productGuidId] = productId;
            TempData[productQuantityId] = quantity;
            TempData[productMealTypeId] = productMealType;
            TempData[mealTypeId] = buttonType;

            return RedirectToAction("AddFood", "Diet");
        }

        [HttpGet]
        public IActionResult AddFood()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFood(Int32 test)
        {
            
            var test1 = TempData[productGuidId] as IEnumerable<Guid>;
            if (test1 != null)
            {
               // TempData[additionalMealsId] = true;
              //  var test44 = test1.ToList();
               // test44.Add(Guid.NewGuid());
               // TempData[productGuidId] = test44;
            }
     
            return RedirectToAction("Day", "Diet");
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
        public IActionResult EditProduct(String productId)
        {
            var product = productManagement.GetProduct(new Guid(productId));
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(String productDeleteId)
        {
            productManagement.RemoveProduct(new Guid(productDeleteId));
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult SubscribeProduct(String productSubscribeId)
        {
            productManagement.SubscribeProduct(new Guid(productSubscribeId));
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult CancelSubscription(String productCancelSubscribeId)
        {
            productManagement.CancelSubscription(new Guid(productCancelSubscribeId));
            return RedirectToAction("ProductList", "Diet");
        }

        /// <summary>
        /// Odpowiedzialny za wyświetlanie listy produktów.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductList()
        {
            var products = productManagement.GetUserProducts();
            var dto = new ProductListDto()
            {
                ProductList = products,
                SelectedProduct = null
            };
            return View(dto);
        }

        /// <summary>
        /// Pozyskuje informacje dotyczące zaznaczonego produktu.
        /// </summary>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductDetails([FromBody]JToken jsonBody)
        {
            var id = jsonBody.Value<String>("Id");
            var productDetails = productManagement.GetProduct(new Guid(id));
            return new JsonResult(productDetails);
        }
    }
}
