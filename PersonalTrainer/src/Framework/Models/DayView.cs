using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class DayView
    {
        public List<DailyProductDto> DailyProduct { get; set; }

        public DateTime Day { get; set; }

        public Decimal DayCalories { get; set; }

        public Decimal DayProteins { get; set; }

        public Decimal DayFat { get; set; }

        public Decimal DayCarbohydrates { get; set; }

        public Decimal DayFibre { get; set; }

        public Decimal AvaibleCalories { get; set; }

        public Decimal AvaibleProteins { get; set; }

        public Decimal AvaibleFat { get; set; }

        public Decimal AvaibleCarbohydrates { get; set; }

        public Decimal AvaibleFibre { get; set; }
    }
}
