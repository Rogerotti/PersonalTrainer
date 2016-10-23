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
        public Decimal Protein { get; set; }

        [Required]
        public Decimal Fat { get; set; }

        [Required]
        public Decimal Carbohydrates { get; set; }

        [Required]
        public Decimal Fibre { get; set; }

        [Required]
        public Decimal Calories { get; set; }

        [Required]
        public Int32 QuantityType { get; set; }

        [Required]
        public Decimal Quantity { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}
