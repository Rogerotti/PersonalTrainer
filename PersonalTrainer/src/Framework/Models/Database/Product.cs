using System;
using System.Collections.Generic;
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

        public Guid UserId { get; set; }

        [Required]
        public String Name { get; set; }

        public String Manufacturer { get; set; }

        [Required]
        public Int32 ProductType { get; set; }

        /// <summary>
        /// Stan produktu.
        /// 0 - Prywatny produkt użytkownika.
        /// 1 - Produkt zlecony do zatwierdzenia przez administratora systemu.
        /// 2 - Produkt odrzucony przez administratora
        /// 3 - Produkt zaakceptowany przez administratora.
        /// </summary>
        [Required]
        public Int32 ProductState {get;set;}

        [Required]
        public virtual ProductDetails ProductDetails { get; set; }
    }
}
