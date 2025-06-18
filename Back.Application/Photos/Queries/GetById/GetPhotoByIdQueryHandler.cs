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

namespace Back.Application.Photos.Queries.GetById
{
    public class GetPhotoByIdQueryHandler
            : IRequestHandler<GetPhotoByIdQuery, PhotoDto>
    {
        private readonly IRepository<Photo> _repo;
        private readonly IMapper _mapper;
        public GetPhotoByIdQueryHandler(IRepository<Photo> repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<PhotoDto> Handle(
            GetPhotoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var photo = await _repo.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new KeyNotFoundException("Photo not found");
            return _mapper.Map<PhotoDto>(photo);
        }
    }
}
