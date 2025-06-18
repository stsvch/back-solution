using AutoMapper;
using Back.Application.Common;
using Back.Application.DTOs;
using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Queries.GetPaged
{
    public class GetDevelopmentsPagedQueryHandler
        : IRequestHandler<GetDevelopmentsPagedQuery, PagedList<DevelopmentDto>>
    {
        private readonly IRepository<Development> _repo;
        private readonly IMapper _mapper;

        public GetDevelopmentsPagedQueryHandler(
            IRepository<Development> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedList<DevelopmentDto>> Handle(
            GetDevelopmentsPagedQuery request,
            CancellationToken cancellationToken)
        {
            var paged = await _repo.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var dtos = paged.Items
                            .Select(dev => _mapper.Map<DevelopmentDto>(dev))
                            .ToList();

            return new PagedList<DevelopmentDto>(dtos, paged.TotalCount);
        }
    }
}
