using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("ProductDetails")]
    public class ProductDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        public Int32 Protein { get; set; }

        [Required]
        public Int32 Fat { get; set; }

        [Required]
        public Int32 Carbohydrates { get; set; }

        [Required]
        public Int32 Fibre { get; set; }

        [Required]
        public Int32 Calories { get; set; }

        [Required]
        public Int32 QuantityType { get; set; }

        [Required]
        public Int32 Quantity { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}
