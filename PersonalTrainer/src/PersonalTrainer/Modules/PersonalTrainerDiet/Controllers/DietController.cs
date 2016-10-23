﻿using Framework.Models;
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
            TempData[productGuidId] = productId.ToList();
            TempData[productQuantityId] = quantity.ToList();
            TempData[productMealTypeId] = productMealType.ToList();
            TempData[mealTypeId] = buttonType;

            return RedirectToAction("AddFood", "Diet");
        }

        [HttpGet]
        public IActionResult AddFood()
        {
            var allProducts = productManagement.GetProducts();
            var userProducts = productManagement.GetUserProducts();
            var dto = new SearchProductsDto()
            {
                AllProducts = allProducts.ToList(),
                UserProducts = userProducts.ToList(),
                RecentProducts = userProducts.ToList()
            };
            return View(dto);
        }

        [HttpPost]
        public IActionResult AddFood(List<Guid> ids, List<Int32> quantity, List<Boolean> checkboxes)
        {
            //Wynika to ze zwracania zaznaczonych checkboxów 2 wartości true oraz false a w przypadku braku zaznaczenia false przez co po wartości true zawsze musi być false.
            List<Boolean> realBooleanValues = new List<Boolean>();
            List<Guid> properIds = new List<Guid>();
            List<Int32> properQuantities = new List<int>();

            if (checkboxes != null && checkboxes.Count < 2)
                realBooleanValues = checkboxes;
            else
            {
                for (int i = 0; i < checkboxes.Count; i++)
                {
                    if (checkboxes[i])
                    {
                        i++;
                        realBooleanValues.Add(true);
                    }
                    else
                        realBooleanValues.Add(false);
                }

            }

            for (int i = 0; i < realBooleanValues.Count(); i++)
            {
                if (realBooleanValues[i])
                {
                    properIds.Add(ids[i]);
                    properQuantities.Add(quantity[i]);
                }
            }

            List<Int32> properMealTypes = new List<int>();
            var enumMealType = TempData[mealTypeId] as Int32?;
            for (int i = 0; i < properIds.Count(); i++)
                properMealTypes.Add((Int32)enumMealType);

            var guids = TempData[productGuidId] as IEnumerable<Guid>;
            if (guids != null && guids.Count() != 0)
            {
                var quants = TempData[productQuantityId] as IEnumerable<Int32>;
                var mealType = TempData[productMealTypeId] as IEnumerable<Int32>;
                properIds.AddRange(guids);
                properQuantities.AddRange(quants);
                properMealTypes.AddRange(mealType);
            }

            TempData[additionalMealsId] = true;
            TempData[productGuidId] = properIds;
            TempData[productQuantityId] = properQuantities;
            TempData[productMealTypeId] = properMealTypes;
            TempData[mealTypeId] = enumMealType;
     
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