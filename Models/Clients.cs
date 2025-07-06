using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarWorkshopApp.Models
{
    public class Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        // Обратная связь с заказами
        public virtual ICollection<Orders> Orders { get; set; }

    }

}
