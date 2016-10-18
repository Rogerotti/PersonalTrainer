using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var additionalMeal = TempData[additionalMealsId] as Boolean?;
            if (additionalMeal == null ? false : (Boolean)additionalMeal)
            {
               var guids = TempData[productGuidId] as IEnumerable<Guid>;
               var quants =  TempData[productQuantityId] as IEnumerable<Int32>;
               var mealType =  TempData[productMealTypeId] as IEnumerable<Int32>;
               var enumMealType = TempData[mealTypeId]  as Int32?;
            }


           
            var mealList = new List<MealDto>();
            var productList = new List<ProductDto>();
            productList.Add(new ProductDto() { Name = "ala", ProductId = Guid.NewGuid(),  Macro = new Macro() { Calories = 5, Fat = 10, Quantity = 10} });
            productList.Add(new ProductDto() { Name = "ala2", ProductId = Guid.NewGuid(), Macro = new Macro() { Calories = 15, Fat = 1, Quantity = 15} });
            var meal1 = new MealDto();
            meal1.Products = productList;
            meal1.MealType = MealType.Breakfast;

            var productList2 = new List<ProductDto>();
            productList2.Add(new ProductDto() { Name = "ala3", ProductId = Guid.NewGuid(), Macro = new Macro() { Calories = 1, Fat = 4, Quantity = 11 } });
            productList2.Add(new ProductDto() { Name = "ala4", ProductId = Guid.NewGuid(), Macro = new Macro() { Calories = 2, Fat = 3, Quantity = 12 } });
            productList2.Add(new ProductDto() { Name = "ala4", ProductId = Guid.NewGuid(), Macro = new Macro() { Calories = 2, Fat = 3, Quantity = 11 } });
            var meal2 = new MealDto();
            meal2.Products = productList2;
            meal2.MealType = MealType.Dinner;


            mealList.Add(meal1);
            mealList.Add(meal2);
            var dto = new DailyFoodDto()
            {
                Meals = mealList
            };
            return View(dto);
        }

        public class Test1
        {
            public Guid ids { get; set; }
            public Int32 quants { get; set; }
            public Int32 meal { get; set; }
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
            TempData[additionalMealsId] = true;
            var test1 = TempData[productGuidId] as IEnumerable<Guid>;
            var test44 = test1.ToList();
            test44.Add(Guid.NewGuid());


            TempData[productGuidId] = test44;
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
            var productDetails = productManagement.GetProduct(new Guid(id));
            return new JsonResult(productDetails);
        }
    }
}
