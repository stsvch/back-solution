using AutoMapper;
using Back.Application.DTOs;
using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Queries.GetById
{
    public class GetCaseStudyByIdQueryHandler
        : IRequestHandler<GetCaseStudyByIdQuery, CaseStudyDto>
    {
        private readonly IContentRepository<CaseStudy> _repo;
        private readonly IMapper _mapper;
        public GetCaseStudyByIdQueryHandler(
            IContentRepository<CaseStudy> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CaseStudyDto> Handle(
            GetCaseStudyByIdQuery request,
            CancellationToken cancellationToken)
        {
            var cs = await _repo.GetByIdWithPhotosAsync(request.Id, cancellationToken)
                     ?? throw new KeyNotFoundException("CaseStudy not found");
            return _mapper.Map<CaseStudyDto>(cs);
        }
    }
}
