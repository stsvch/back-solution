using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommandHandler
        : IRequestHandler<CreatePhotoCommand, Guid>
    {
        private readonly IRepository<Photo> _repo;
        public CreatePhotoCommandHandler(IRepository<Photo> repo) => _repo = repo;

        public async Task<Guid> Handle(
            CreatePhotoCommand request,
            CancellationToken cancellationToken)
        {
            var photo = new Photo(request.Path);
            await _repo.AddAsync(photo, cancellationToken);
            return photo.Id;
        }
    }
}
