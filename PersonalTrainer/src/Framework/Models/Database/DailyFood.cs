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
        public Guid DayId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Int32 TotalCalories { get; set; }

        public Int32 TotalProteins { get; set; }

        public Int32 TotalFat { get; set; }

        public Int32 TotalFibre { get; set; }

        public Int32 TotalCarbohydrates { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual ICollection<Product> Product { get; set; }
    }
}
