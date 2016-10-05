using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.Database
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }

        [Required]
        public String UserName { get; set; }

        [Required]
        public String HashCode { get; set; }

        [Required]
        public String Salt { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public virtual UserDetails UserDetails { get; set; }
    }
}
