using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("DailyFoodProduct")]
    public class DailyFoodProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("DailyFood")]
        public Guid DailyFoodId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public Int32 MealType { get; set; }

        public Int32 Quantity { get; set; }

        public virtual DailyFood DailyFood { get; set; }

        public virtual Product Product { get; set; }
    }
}
