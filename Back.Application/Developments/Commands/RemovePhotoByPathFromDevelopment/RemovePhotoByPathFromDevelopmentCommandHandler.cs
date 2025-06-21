using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.RemovePhotoByPathFromDevelopment
{
    public class RemovePhotoByPathFromDevelopmentCommandHandler
            : IRequestHandler<RemovePhotoByPathFromDevelopmentCommand, Unit>
    {
        private readonly IContentRepository<Development> _repo;

        public RemovePhotoByPathFromDevelopmentCommandHandler(
            IContentRepository<Development> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            RemovePhotoByPathFromDevelopmentCommand request,
            CancellationToken ct)
        {
            var dev = await _repo.GetByIdWithPhotosAsync(request.DevelopmentId, ct)
                      ?? throw new KeyNotFoundException("Development not found");

            var photo = dev.Photos.FirstOrDefault(p => p.Path == request.Path)
                        ?? throw new KeyNotFoundException("Photo not found with given path");

            dev.RemovePhoto(photo);
            await _repo.UpdateAsync(dev, ct);
            return Unit.Value;
        }
    }
}
