using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class DailyProductDto
    {
        public MealType MealType { get; set; }

        public ProductDto Product { get; set; }

        public Macro CurrentMacro { get; set; }
    }
}
