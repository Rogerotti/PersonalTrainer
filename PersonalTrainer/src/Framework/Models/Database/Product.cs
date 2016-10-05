using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public String Name { get; set; }

        public  String Manufacturer { get; set; }

        [Required]
        public Int32 ProductType { get; set; }

        [Required]
        public virtual ProductDetails ProductDetails { get; set; }
    }
}
