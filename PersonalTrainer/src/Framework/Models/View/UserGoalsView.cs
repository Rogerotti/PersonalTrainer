using System;

namespace Framework.Models.View
{
    public class UserGoalsView
    {
        public Guid UserId { get; set; }

        public Int32 Proteins { get; set; }

        public Int32 Fat { get; set; }

        public Int32 Carbohydrates { get; set; }

        public Int32 Calories { get; set; }

        public Decimal PercentageFat { get; set; }

        public Decimal PercentageProtein { get; set; }

        public Decimal PercentageCarbs { get; set; }
    }
}
