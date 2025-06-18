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

namespace Back.Application.Photos.Queries.GetPhotosPaged
{
    public class GetPhotosPagedQueryHandler
        : IRequestHandler<GetPhotosPagedQuery, PagedList<PhotoDto>>
    {
        private readonly IRepository<Photo> _repo;
        private readonly IMapper _mapper;
        public GetPhotosPagedQueryHandler(IRepository<Photo> repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<PagedList<PhotoDto>> Handle(
            GetPhotosPagedQuery request,
            CancellationToken cancellationToken)
        {
            var paged = await _repo.GetPagedAsync(
                request.PageNumber, request.PageSize, cancellationToken);

            var dtos = paged.Items
                            .Select(p => _mapper.Map<PhotoDto>(p))
                            .ToList();
            return new PagedList<PhotoDto>(dtos, paged.TotalCount);
        }
    }
}
