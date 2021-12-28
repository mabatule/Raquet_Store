using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Models
{
    public class RaquetModel
    {
        public long id { get; set; }
        public string modelo { get; set; }
        public float? peso { get; set; } 
        public int? precio { get; set; }
        public string descripcion { get; set; }
        public int cantidad{ get; set; }
        public string MainPhoto { get; set; }
        public string SecondPhoto { get; set; }
        public long BrandId { get; set; }

    }
}
