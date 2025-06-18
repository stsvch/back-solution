using Back.Application.Common;
using Back.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Queries.GetPaged
{
    public record GetDevelopmentsPagedQuery(int PageNumber, int PageSize)
        : IRequest<PagedList<DevelopmentDto>>;
}
