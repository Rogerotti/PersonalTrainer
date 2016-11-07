using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    /// <summary>
    /// Tabela przedstawiająca cele użytkowników.
    /// </summary>
    [Table(nameof(UserGoal))]
    public class UserGoal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey(nameof(Database.User))]
        public Guid UserId { get; set; }

        public Decimal Proteins { get; set; }

        public Decimal Fat { get; set; }

        public Decimal Carbohydrates { get; set; }

        public Decimal Fibre { get; set; }

        public Int32 Calories { get; set; }

        public Int32 Weight { get; set; }

        public Decimal BodyFat { get; set; }

        /// <summary>
        /// Użytkownik.
        /// </summary>
        [Required]
        public virtual User User { get; set; }
    }
}

