using Back.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Queries.GetById
{
    public record GetCaseStudyByIdQuery(Guid Id) : IRequest<CaseStudyDto>;
}
