using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.RemovePhotoByPathFromCaseStudy
{
    public record RemovePhotoByPathFromCaseStudyCommand(
        Guid CaseStudyId,
        string Path
    ) : IRequest<Unit>;
}
