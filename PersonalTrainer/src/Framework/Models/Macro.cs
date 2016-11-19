﻿using System;

namespace Framework.Models
{
    public class Macro
    {
        private const Int32 fatRation = 9;
        private const Int32 proteinRatio = 4;
        private const Int32 carbohydratesRatio = 4;
        private const Int32 fibreRatio = 2;

        public Decimal Protein { get; set; }

        public Decimal Fat { get; set; }

        public Decimal Carbohydrates { get; set; }

        public Decimal Fibre { get; set; }

        public Decimal Calories { get; set; }

        public QuantityType QuantityType { get; set; }

        public Int32 Quantity { get; set; }
        

        public void CountCalories()
        {
            var previousQuantity = Quantity;

            var proteinCalories = Protein > 0 ? Protein * proteinRatio : 0;
            var fatCalories = Fat > 0 ? Fat * fatRation : 0;
            var carbohydratesCalories = Carbohydrates > 0 ? Carbohydrates * carbohydratesRatio : 0;
            var fibreCalories = Fibre > 0 ? Fibre * fibreRatio : 0;

            Calories = proteinCalories + fatCalories + carbohydratesCalories + fibreCalories;
        }
    }
}
