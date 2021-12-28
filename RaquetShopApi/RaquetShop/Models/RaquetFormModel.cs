using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace RaquetShop.Models
{
    public class RaquetFormModel:RaquetModel
    {
        public IFormFile MainImagen { get; set; }
        public IFormFile SecondImagen { get; set; }
    }
}
