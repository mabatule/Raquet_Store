using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Data.Entities
{
    public class RaquetEntity
    {
        [Key]
        [Required]
        public long id { get; set; }
        public string modelo { get; set; }
        public float? peso { get; set; }
        public int? precio { get; set; }
        public string descripcion { get; set; }
        public int? cantidad { get; set; }
        public string MainPhoto { get; set; }
        public string SecondPhoto { get; set; }
        [ForeignKey("brandId")]
        public virtual BrandEntity Brand { get; set; }
    }
}
