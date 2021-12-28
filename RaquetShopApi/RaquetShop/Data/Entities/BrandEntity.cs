using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Data.Entities
{
    public class BrandEntity
    {
        [Key]
        [Required]
        public long id { get; set; }
        public string nombre { get; set; }
        public string pais { get; set; }
        public string descripcion { get; set; }
        public ICollection<RaquetEntity> raquets { get; set; }

    }
}
