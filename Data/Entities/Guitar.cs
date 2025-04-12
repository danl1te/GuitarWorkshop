using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuitarWorkshop.Data.Entities
{
    public class Guitar
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int Year { get; set; }

        [StringLength(50)]
        public string? SerialNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string Type { get; set; } = "Acoustic";

        [StringLength(500)]
        public string? ConditionDescription { get; set; }

        public int? ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}