using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Models
{
    public class BrandModel
    {
        public long id { get; set; }
        public string nombre { get; set; } 
        public string pais { get; set; }
        public string descripcion{ get; set; }

    }
}
