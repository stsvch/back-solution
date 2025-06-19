using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.RemovePhotoFromCaseStudy
{
    public class RemovePhotoFromCaseStudyCommandHandler
            : IRequestHandler<RemovePhotoFromCaseStudyCommand, Unit>
    {
        private readonly IContentRepository<CaseStudy> _repo;

        public RemovePhotoFromCaseStudyCommandHandler(
            IContentRepository<CaseStudy> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            RemovePhotoFromCaseStudyCommand request,
            CancellationToken ct)
        {
            var cs = await _repo.GetByIdWithPhotosAsync(
                        request.CaseStudyId, ct)
                     ?? throw new KeyNotFoundException("CaseStudy not found");

            var photo = cs.Photos.FirstOrDefault(p => p.Id == request.PhotoId)
                        ?? throw new KeyNotFoundException("Photo not found");

            cs.RemovePhoto(photo);
            await _repo.UpdateAsync(cs, ct);

            return Unit.Value;
        }
    }
}
