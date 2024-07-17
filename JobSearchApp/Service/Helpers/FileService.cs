using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Helpers.Interfaces;

namespace Service.Helpers
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnviorment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviorment = webHostEnvironment;
        }

        public string CreateImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + ".jpeg";
            string path = Path.Combine(_webHostEnviorment.WebRootPath, "images", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public void DeleteImage(string fileName)
        {
            if (System.IO.File.Exists(Path.Combine(_webHostEnviorment.WebRootPath, "images", fileName)))
            {
                System.IO.File.Delete(Path.Combine(_webHostEnviorment.WebRootPath, "images", fileName));
            }
        }

        public bool IsImage(IFormFile file)
        {
            return file.ContentType.Contains("image") ? true : false;
        }

        public bool IsLengthSuit(IFormFile file, int value)
        {
            return file.Length / 1024 < value ? true : false;
        }
    }
}

