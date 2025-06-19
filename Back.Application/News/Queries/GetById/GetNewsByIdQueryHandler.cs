using AutoMapper;
using Back.Application.DTOs;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Queries.GetById
{
    public class GetNewsByIdQueryHandler
        : IRequestHandler<GetNewsByIdQuery, NewsDto>
    {
        private readonly IContentRepository<Back.Domain.Entities.News> _repo;
        private readonly IMapper _mapper;

        public GetNewsByIdQueryHandler(
            IContentRepository<Back.Domain.Entities.News> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<NewsDto> Handle(
            GetNewsByIdQuery request,
            CancellationToken cancellationToken)
        {
            var news = await _repo.GetByIdWithPhotosAsync(request.Id, cancellationToken)
                       ?? throw new KeyNotFoundException("News not found");
            return _mapper.Map<NewsDto>(news);
        }
    }
}
