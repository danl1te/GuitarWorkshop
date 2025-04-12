using System;
using System.ComponentModel.DataAnnotations;

    namespace GuitarWorkshop.Data.Entities
    {
        public class Order
        {
            public int Id { get; set; }

            public DateTime OrderDate { get; set; } = DateTime.Now;
            public DateTime? CompletionDate { get; set; }

            [Required]
            [StringLength(20)]
            public string Status { get; set; } = "Новый";

            [StringLength(1000)]
            public string? Description { get; set; }

            [Range(0, 1000000)]
            public decimal Price { get; set; }

            public int ClientId { get; set; }
            public int GuitarId { get; set; }
            public int? RepairServiceId { get; set; }

            public virtual Client Client { get; set; }
            public virtual Guitar Guitar { get; set; }
            public virtual RepairService? RepairService { get; set; }
        }
    }
