using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    /// <summary>
    /// Tabela produktów dnia żywieniowego.
    /// </summary>
    [Table(nameof(DiaryProduct))]
    public class DiaryProduct
    {
        /// <summary>
        /// Id produktu żywieniowego.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DiaryProductId { get; set; }

        /// <summary>
        /// Id dnia żywieniowego.
        /// </summary>
        [ForeignKey(nameof(DayFoodDiary))]
        public Guid DayId { get; set; }

        /// <summary>
        /// Id produktu.
        /// </summary>
        [ForeignKey(nameof(Database.Product))]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Typ produktu.
        /// </summary>
        public Int32 MealType { get; set; }

        /// <summary>
        /// Ilość produktu.
        /// </summary>
        public Int32 Quantity { get; set; }

        public virtual DayFoodDiary Day { get; set; }

        public virtual Product Product { get; set; }
    }
}
