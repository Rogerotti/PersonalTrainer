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
        private readonly IUserGoalsManagement userGoalsManamgenet;

        private const String additionalMealsId = nameof(additionalMealsId);
        private const String productGuidId = nameof(productGuidId);
        private const String productQuantityId = nameof(productQuantityId);
        private const String productMealTypeId = nameof(productMealTypeId);
        private const String mealTypeId = nameof(mealTypeId);

        public DietController(IProductManagement productManagement,
            IUserGoalsManagement userGoalsManamgenet)
        {
            this.productManagement = productManagement;
            this.userGoalsManamgenet = userGoalsManamgenet;
        }

        public IActionResult Index()
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
                if (guids != null && guids.Count() != 0)
                {
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
            }

            if (ProductDto == null)
                ProductDto = productManagement.GetDailyFood(DateTime.Today);


            var userGoals = userGoalsManamgenet.GetCurrentUserGoals();

            var dayView = new DayView()
            {
                DayProteins = ProductDto.DayProteins,
                DayFibre = ProductDto.DayFibre,
                DayFat = ProductDto.DayFat,
                DailyProduct = ProductDto.DailyProduct,
                DayCarbohydrates = ProductDto.DayCarbohydrates,
                Day = ProductDto.Day,
                DayCalories = ProductDto.DayCalories,
                AvaibleCalories = userGoals.Calories,
                AvaibleCarbohydrates = userGoals.Carbohydrates,
                AvaibleFat = userGoals.Fat,
                AvaibleProteins = userGoals.Proteins,
                AvaibleFibre = userGoals.Proteins
            };

            return View(dayView);
        }

        [HttpPost]
        public IActionResult Day(IEnumerable<Guid> productId, IEnumerable<Int32> quantity, IEnumerable<Int32> productMealType, Int32 buttonType)
        {
            if (buttonType == 4)
            {
                var idList = productId.ToList();
                var quantityList = quantity.ToList();
                var mealTypeList = productMealType.ToList();
                List<DailyFoodProductDto> food = new List<DailyFoodProductDto>();
                for (int i = 0; i < productId.Count(); i++)
                {
                    food.Add(new DailyFoodProductDto()
                    {
                        ProductId = idList[i],
                        ProductQuantity = quantityList[i],
                        MealType = (MealType)mealTypeList[i]
                    });
                }
                productManagement.SubmitDailyFood(DateTime.Today, food);

                return RedirectToAction("Day", "Diet");
            }


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
            return View(new ProductDto() {Macro = new Macro() });
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDto dto)
        {
            try
            {
                productManagement.AddProduct(dto);
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("AdditionalValidation", exc.Message);
                return View(dto);
            }
            return RedirectToAction("ProductList", "Diet");
        }

        [HttpPost]
        public IActionResult EditProduct2(ProductDto product)
        {
            try
            { 
                productManagement.UpdateProduct(product);
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("AdditionalValidation", exc.Message);
                return View(product);
            }
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

        [HttpGet]
        public IActionResult UserGoals()
        {
            var goals = userGoalsManamgenet.GetCurrentUserGoals();


            if (goals.Calories == 0)
                goals.Calories = 1;

            var view = new UserGoalsView()
            {
                BodyFat = goals.BodyFat,
                Calories = goals.Calories,
                Carbohydrates = goals.Carbohydrates,
                Fat = goals.Fat,
                Fibre = goals.Fibre,
                Proteins = goals.Proteins,
                UserId = goals.UserId,
                Weight = goals.Weight,
                PercentageCarbs = goals.Carbohydrates * 3 * 100 / goals.Calories,
                PercentageFat = goals.Fat * 4 * 100 / goals.Calories,
                PercentageFibre = goals.Fibre * 3 * 100 / goals.Calories,
                PercentageProtein = goals.Proteins * 3 * 100 / goals.Calories,
            };

            return View(view);
        }

        [HttpPost]
        public IActionResult UserGoals(UserGoalsView dto)
        {
            if (dto != null)
            {
                try
                {
                   var userGoals = new UserGoalsDto()
                    {
                        BodyFat = dto.BodyFat,
                        Calories = dto.Calories,
                        Weight = dto.Weight,
                        Fat = dto.Fat,
                        Carbohydrates = dto.Carbohydrates,
                        Fibre = dto.Fibre,
                        Proteins = dto.Proteins,
                        UserId = dto.UserId
                    };
                    userGoalsManamgenet.SetGoals(userGoals);
                }
                catch(Exception exc)
                {
                    ModelState.TryAddModelError("AdditionalValidation", exc.Message);
                }
            }
          
            return View(dto);
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
