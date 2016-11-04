using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table(nameof(DailyFoodProduct))]
    public class DailyFoodProduct
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public Guid DailyFoodProductId { get; set; }

        [Key]
        [ForeignKey(nameof(DailyFood))]
        public Guid DailyFoodId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public Int32 MealType { get; set; }

        public Int32 Quantity { get; set; }

        public virtual DailyFood DailyFood { get; set; }

        public virtual Product Product { get; set; }
    }
}
