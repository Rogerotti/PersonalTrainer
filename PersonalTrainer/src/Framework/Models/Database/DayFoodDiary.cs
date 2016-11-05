﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    /// <summary>
    /// Tabela przedstawiająca dziennik okna żywieniowego użytkowika dla danego dnia.
    /// </summary>
    [Table(nameof(DayFoodDiary))]
    public class DayFoodDiary
    {
        /// <summary>
        /// Id dnia żywieniowego.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid DayId { get; set; }

        /// <summary>
        /// Id użytkownika którego okno żywieniowe dotyczy.
        /// </summary>
        [ForeignKey(nameof(Database.User))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Data dnia żywienionwego.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Spożyte kalorie w ciągu dnia.
        /// </summary>
        public Decimal TotalCalories { get; set; }

        /// <summary>
        /// Spożyte białko w ciągu dnia.
        /// </summary>
        public Decimal TotalProteins { get; set; }

        /// <summary>
        /// Spożyty tłuszcz w ciągu dnia.
        /// </summary>
        public Decimal TotalFat { get; set; }

        /// <summary>
        /// Spożyty błonnik w ciągu dnia.
        /// </summary>
        public Decimal TotalFibre { get; set; }

        /// <summary>
        /// Spożyte węglowodany w ciągu dnia.
        /// </summary>
        public Decimal TotalCarbohydrates { get; set; }

        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// Lista posiłków spożytych podczas danego dnia żywieniowego.
        /// </summary>
        public virtual ICollection<DiaryProduct> DailyFoodProducts { get; set; }
    }
}
