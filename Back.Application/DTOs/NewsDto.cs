// Back.Application.DTOs/NewsDto.cs
using System;
using System.Collections.Generic;

namespace Back.Application.DTOs
{
    public class NewsDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<string> PhotoPaths { get; init; }

        // если у вас есть ещё поля Slug, Date и т.п. – тоже объявите их здесь:
        // public string Slug { get; init; }
        // public DateTime PublishedAt { get; init; }

        public NewsDto()
        {
            PhotoPaths = Array.Empty<string>();
        }
    }
}
