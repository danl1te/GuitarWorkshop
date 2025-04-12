using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuitarWorkshop.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Notes { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}