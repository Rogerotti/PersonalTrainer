using System;

namespace Framework.Models.Dto
{
    public class UserGoalsDto
    {
        public Guid UserId { get; set; }

        public Int32 Proteins { get; set; }

        public Int32 Fat { get; set; }

        public Int32 Carbohydrates { get; set; }

        public Int32 Calories { get; set; }

        //TODO waga i bodyfat
        //public Int32 Weight { get; set; }

        //public Decimal BodyFat { get; set; }
    }
}
