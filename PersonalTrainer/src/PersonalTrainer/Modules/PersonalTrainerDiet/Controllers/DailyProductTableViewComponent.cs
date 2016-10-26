using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalTrainerDiet.Controllers
{
    public class DailyProductTableViewComponent : ViewComponent
    {
        private readonly IProductManagement productManagement;
        public DailyProductTableViewComponent(IProductManagement productManagement)
        {
            this.productManagement = productManagement;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<DailyProductDto> test = new List<DailyProductDto>();
            test.Add(new DailyProductDto()
            {
                MealType = MealType.Breakfast,
                Product = new ProductDto()
                {
                    Macro = new Macro()
                    {
                        Calories = 100,
                        Fat = 100,
                        Carbohydrates = 100,
                        Fibre = 100,
                        Protein = 100,
                        Quantity = 100,
                        QuantityType = QuantityType.Grams
                    },
                    Manufacturer = "test",
                    Name = "te",
                    ProductId = new Guid(),
                    State = ProductState.Accepted,
                    Type = ProductType.DairyProducts,
                    UserId = new Guid()
                }
            });
            return await Task.Run(() =>
            {
                return View(new DailyFoodDto()
                {
                    DayCalories = 0,
                    DayCarbohydrates = 0,
                    DayFat = 0,
                    DayFibre = 0,
                    DayProteins = 0,
                    DailyProduct = test,
                    Day = DateTime.Now
                });
               // return View(productManagement.GetDailyFood(DateTime.Today));
            });
        }
    }
}
