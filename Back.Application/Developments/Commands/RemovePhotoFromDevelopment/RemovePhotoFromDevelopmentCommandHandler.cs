using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.RemovePhotoFromDevelopment
{
    public class RemovePhotoFromDevelopmentCommandHandler
            : IRequestHandler<RemovePhotoFromDevelopmentCommand, Unit>
    {
        private readonly IContentRepository<Development> _repo;
        public RemovePhotoFromDevelopmentCommandHandler(
            IContentRepository<Development> repo) => _repo = repo;

        public async Task<Unit> Handle(
            RemovePhotoFromDevelopmentCommand request,
            CancellationToken ct)
        {
            var dev = await _repo.GetByIdWithPhotosAsync(request.DevelopmentId, ct)
                      ?? throw new KeyNotFoundException("Development not found");

            var photo = dev.Photos.FirstOrDefault(p => p.Id == request.PhotoId)
                        ?? throw new KeyNotFoundException("Photo not found in this Development");

            dev.RemovePhoto(photo);
            await _repo.UpdateAsync(dev, ct);
            return Unit.Value;
        }
    }
}
