using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Models.Database
{
    [Table("ProductMeal")]
    public class ProductMeal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Meal")]
        public Guid MealId { get; set; }

        public virtual Meal Meal { get; set; }
    }
}
