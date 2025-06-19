using AutoMapper;
using Back.Application.Common;
using Back.Application.DTOs;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Queries.GetPaged
{
    public class GetNewsPagedQueryHandler
        : IRequestHandler<GetNewsPagedQuery, PagedList<NewsDto>>
    {
        private readonly IContentRepository<Back.Domain.Entities.News> _repo;
        private readonly IMapper _mapper;

        public GetNewsPagedQueryHandler(
            IContentRepository<Back.Domain.Entities.News> repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedList<NewsDto>> Handle(
            GetNewsPagedQuery request,
            CancellationToken cancellationToken)
        {
            var paged = await _repo.GetPagedWithPhotosAsync(
                request.PageNumber, request.PageSize, cancellationToken);

            var list = paged.Items
                            .Select(n => _mapper.Map<NewsDto>(n))
                            .ToList();

            return new PagedList<NewsDto>(list, paged.TotalCount);
        }
    }
}
