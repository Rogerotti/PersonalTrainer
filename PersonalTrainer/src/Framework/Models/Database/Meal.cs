using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("Meal")]
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MealId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Int32 MealType { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
