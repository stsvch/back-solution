using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.RemovePhotoFromCaseStudy
{
    public record RemovePhotoFromCaseStudyCommand(
        Guid CaseStudyId,
        Guid PhotoId
    ) : IRequest<Unit>;
}
