using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class DailyFoodProductDto
    {
        public Guid ProductId { get; set; }

        public Int32 ProductQuantity { get; set; }

        public MealType MealType { get; set; }
    }
}
