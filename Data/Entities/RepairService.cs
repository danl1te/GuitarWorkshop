using System.ComponentModel.DataAnnotations;

namespace GuitarWorkshop.Data.Entities
{
    public class RepairService
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, 1000000)]
        public decimal BasePrice { get; set; }

        [Range(0, 1000)]
        public int EstimatedDurationHours { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } // Настройка, Ремонт, Модификация
    }
}