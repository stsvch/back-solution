// Back.Application.DTOs/DevelopmentDto.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Back.Application.DTOs
{
    public record DevelopmentDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<string> PhotoPaths { get; init; }

        // parameterless constructor для AutoMapper
        public DevelopmentDto()
        {
            PhotoPaths = Enumerable.Empty<string>();
        }
    }
}
