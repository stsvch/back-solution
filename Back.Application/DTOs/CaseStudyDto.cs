using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.DTOs
{
    public record CaseStudyDto(
        Guid Id,
        string Title,
        string Description,
        IEnumerable<string> PhotoPaths
    );
}
