using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class UserGoalsDto
    {
        public Guid UserId { get; set; }

        public Decimal Proteins { get; set; }

        public Decimal Fat { get; set; }

        public Decimal Carbohydrates { get; set; }

        public Decimal Fibre { get; set; }

        public Int32 Calories { get; set; }

        public Int32 Weight { get; set; }

        public Decimal BodyFat { get; set; }
    }
}
