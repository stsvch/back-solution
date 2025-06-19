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

namespace Back.Application.CaseStudies.Queries.GetPaged
{
    public class GetCaseStudiesPagedQueryHandler
            : IRequestHandler<GetCaseStudiesPagedQuery, PagedList<CaseStudyDto>>
    {
        private readonly IContentRepository<CaseStudy> _repo;
        private readonly IMapper _mapper;
        public GetCaseStudiesPagedQueryHandler(
            IContentRepository<CaseStudy> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedList<CaseStudyDto>> Handle(
            GetCaseStudiesPagedQuery request,
            CancellationToken cancellationToken)
        {
            var paged = await _repo.GetPagedWithPhotosAsync(
                request.PageNumber, request.PageSize, cancellationToken);

            var dtos = paged.Items
                            .Select(cs => _mapper.Map<CaseStudyDto>(cs))
                            .ToList();

            return new PagedList<CaseStudyDto>(dtos, paged.TotalCount);
        }
    }
}
