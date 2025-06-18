using Back.Application.Common;
using Back.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Queries.GetPaged
{
    public record GetCaseStudiesPagedQuery(int PageNumber, int PageSize)
        : IRequest<PagedList<CaseStudyDto>>;
}
