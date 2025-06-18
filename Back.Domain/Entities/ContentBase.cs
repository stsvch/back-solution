using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Entities
{
    public abstract class ContentBase : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        // Навигация к фотографиям
        private readonly List<Photo> _photos = new();
        public IReadOnlyCollection<Photo> Photos => _photos.AsReadOnly();

        // Для EF Core
        protected ContentBase() { }

        protected ContentBase(string title, string description)
        {
            ChangeTitle(title);
            ChangeDescription(description);
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));
            Title = title;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));
            Description = description;
        }

        public void AddPhoto(string path)
        {
            var photo = new Photo(path);
            _photos.Add(photo);
        }

        public void RemovePhoto(Photo photo)
        {
            _photos.Remove(photo);
        }
    }
}
