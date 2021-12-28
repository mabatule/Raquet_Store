using Microsoft.AspNetCore.Http;
using RaquetShop.Exceptions;
using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Services
{
    public class FileService : IFileService
    {
        public string UploadFile(IFormFile file, string folderName)
        {
            if (file == null)
                throw new NotFoundFileException($"The file is invalid.");
            string imagePath = string.Empty;
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {

                string extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                var fullPath = Path.Combine(pathToSave, fileName);
                imagePath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return imagePath;
        }
        public RaquetFormModel createFilesToRaquet(RaquetFormModel newRaquet)
        {
            var file = newRaquet.MainImagen;
            var fileSecond = newRaquet.SecondImagen;
            var folderName = Path.Combine("Resources", "Images", "Raquets");
            string imagePath = "";
            string imagePathSecond = "";

            if (file != null)
                imagePath = UploadFile(file, folderName);
            else
                imagePath = folderName + "/vacio.jpg";
            if (fileSecond != null)
                imagePathSecond = UploadFile(fileSecond, folderName);
            else
                imagePathSecond = folderName + "/vacio.jpg";

            newRaquet.MainPhoto = imagePath;
            newRaquet.SecondPhoto = imagePathSecond;
            return newRaquet;
        }

        public RaquetFormModel updateFilesToRaquet(RaquetFormModel newRaquet)
        {
            var file = newRaquet.MainImagen;
            var fileSecond = newRaquet.SecondImagen;
            var folderName = Path.Combine("Resources", "Images", "Raquets");
            string imagePath;
            string imagePathSecond;
            if (file != null)
            {
                imagePath = UploadFile(file, folderName);
                newRaquet.MainPhoto = imagePath;
            }
            if (fileSecond != null)
            {
                imagePathSecond = UploadFile(fileSecond, folderName);
                newRaquet.SecondPhoto = imagePathSecond;
            }
            return newRaquet;
        }

      
    }
}
