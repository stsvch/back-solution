using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Entities
{
    public class Photo : BaseEntity
    {
        /// <summary>
        /// Путь к файлу (относительный URL или файловая система).
        /// </summary>
        public string Path { get; private set; }

        // Конструктор для EF Core
        protected Photo() { }

        public Photo(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path cannot be empty", nameof(path));

            Path = path;
        }

        // Back.Domain/Entities/Photo.cs
        public void ChangePath(string newPath)
        {
            if (string.IsNullOrWhiteSpace(newPath))
                throw new ArgumentException("Path is required", nameof(newPath));
            Path = newPath;
        }

    }
}
