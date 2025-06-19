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

namespace Back.Application.Developments.Queries.GetById
{
    public class GetDevelopmentByIdQueryHandler
        : IRequestHandler<GetDevelopmentByIdQuery, DevelopmentDto>
    {
        private readonly IContentRepository<Development> _repo;
        private readonly IMapper _mapper;

        public GetDevelopmentByIdQueryHandler(
            IContentRepository<Development> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DevelopmentDto> Handle(
            GetDevelopmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var dev = await _repo.GetByIdWithPhotosAsync(request.Id, cancellationToken)
                      ?? throw new KeyNotFoundException("Development not found");
            return _mapper.Map<DevelopmentDto>(dev);
        }
    }
}
