using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.DeleteCaseStudy
{
    public record DeleteCaseStudyCommand(Guid Id) : IRequest<Unit>;
}
