using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarWorkshopApp.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal Cost { get; set; }
        public DateTime Deadline { get; set; }

        // Связь с гитарой
        public int GuitarId { get; set; }
        public virtual Guitars Guitar { get; set; }

        // Связь с клиентом
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Clients Client { get; set; }
    }

}
