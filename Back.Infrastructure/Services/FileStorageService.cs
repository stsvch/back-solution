using Back.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _uploadsFolder;
        public FileStorageService(IWebHostEnvironment env)
        {
            _uploadsFolder = Path.Combine(env.ContentRootPath, "Uploads");
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            // создаём уникальное имя файла
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(_uploadsFolder, fileName);
            // сохраняем на диск
            await using var fs = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fs);
            // возвращаем относительный URL
            return $"/uploads/{fileName}";
        }
    }
}
