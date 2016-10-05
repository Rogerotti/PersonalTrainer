using System;

namespace Framework.Models
{
    public class Macro
    {
        private const Int32 fatRation = 9;
        private const Int32 proteinRatio = 4;
        private const Int32 carbohydratesRatio = 4;
        private const Int32 fibreRatio = 2;

        public Int32 Protein { get; set; }

        public Int32 Fat { get; set; }

        public Int32 Carbohydrates { get; set; }

        public Int32 Fibre { get; set; }

        public Int32 Calories { get; set; }

        public QuantityType QuantityType { get; set; }

        public Int32 Quantity { get; set; }

        public void CountCalories()
        {
            var proteinCalories = Protein > 0 ? Protein * proteinRatio : 0;
            var fatCalories = Fat > 0 ? Fat * fatRation : 0;
            var carbohydratesCalories = Carbohydrates > 0 ? Carbohydrates * carbohydratesRatio : 0;
            var fibreCalories = Fibre > 0 ? Fibre * fibreRatio : 0;

            Calories = proteinCalories + fatCalories + carbohydratesCalories + fibreCalories;
        }

    }
}
