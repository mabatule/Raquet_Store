using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RaquetShop.Models;
namespace RaquetShop.Services
{
    public interface IFileService
    {
        string UploadFile(IFormFile file, string folderName);
        public RaquetFormModel createFilesToRaquet(RaquetFormModel newRaquet);  
        public RaquetFormModel updateFilesToRaquet(RaquetFormModel newRaquet); 

    }
}
