﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("UserDetails")]
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Płeć
        /// Zgodnie z ISO/IEC 5218
        /// 0 - nie wiadomo.
        /// 1 = meżczyzna.
        /// 2 = kobieta.
        /// 9 = nie zaaplikowano.
        /// </summary>
        public Int32 Gender { get; set; }

        /// <summary>
        /// Waga w kilogramach.
        /// </summary>
        public Decimal Weight { get; set; }

        /// <summary>
        /// Wzrost
        /// </summary>
        public Decimal Height { get; set; }

        /// <summary>
        /// Jednostka wzrostu
        /// 0 - nie ustawiono
        /// 1 - centymetry.
        /// 2 - stopy.
        /// 3 - yardy.
        /// </summary>
        public Int32 HeightUnit { get; set; }

        /// <summary>
        /// Wiek
        /// </summary>
        public Int32 Age { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}