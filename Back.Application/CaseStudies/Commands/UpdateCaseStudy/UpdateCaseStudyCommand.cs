﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.UpdateCaseStudy
{
    public record UpdateCaseStudyCommand(
        Guid Id,
        string Title,
        string Description
    ) : IRequest<Unit>;
}
