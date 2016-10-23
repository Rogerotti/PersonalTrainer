using System;
using System.Collections.Generic;

namespace Framework.Models
{
    public class DailyFoodDto
    {
        public List<DailyProductDto> DailyProduct { get; set; }

        public DateTime Day { get; set; }

        public Int32 DayCalories { get; set; }

        public Int32 DayProteins { get; set; }

        public Int32 DayFat { get; set; }

        public Int32 DayCarbohydrates { get; set; }

        public Int32 DayFibre { get; set; }

    }
}
