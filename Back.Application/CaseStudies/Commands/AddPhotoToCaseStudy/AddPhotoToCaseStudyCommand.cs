using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.AddPhotoToCaseStudy
{
    public record AddPhotoToCaseStudyCommand(
        Guid CaseStudyId,
        string PhotoPath
    ) : IRequest<Guid>;
}
