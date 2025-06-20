// Back.Application.DTOs/CaseStudyDto.cs
using System;
using System.Collections.Generic;

public class CaseStudyDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public IEnumerable<string> PhotoPaths { get; init; }

    public CaseStudyDto() => PhotoPaths = Array.Empty<string>();
}
