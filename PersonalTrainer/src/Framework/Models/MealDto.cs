using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class MealDto
    {
        public Guid MealId { get; set; }


        public IEnumerable<ProductDto> Products;

        public MealType MealType { get; set; }

        public Int32 DayCalories { get; set; }

        public Int32 DayProteins { get; set; }

        public Int32 DayFat { get; set; }

        public Int32 DayCarbohydrates { get; set; }

        public Int32 DayFibre { get; set; }
    }
}
