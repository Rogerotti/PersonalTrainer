using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("DailyFood")]
    public class DailyFood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid DailyFoodId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Decimal TotalCalories { get; set; }

        public Decimal TotalProteins { get; set; }

        public Decimal TotalFat { get; set; }

        public Decimal TotalFibre { get; set; }

        public Decimal TotalCarbohydrates { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<DailyFoodProduct> DailyFoodProducts { get; set; }
    }
}
