using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.AddPhotoToCaseStudy
{
    public class AddPhotoToCaseStudyCommandHandler
            : IRequestHandler<AddPhotoToCaseStudyCommand, Guid>
    {
        private readonly IContentRepository<CaseStudy> _repo;

        public AddPhotoToCaseStudyCommandHandler(
            IContentRepository<CaseStudy> repo)
            => _repo = repo;

        public async Task<Guid> Handle(
            AddPhotoToCaseStudyCommand request,
            CancellationToken ct)
        {
            var cs = await _repo.GetByIdWithPhotosAsync(
                        request.CaseStudyId, ct)
                     ?? throw new KeyNotFoundException("CaseStudy not found");

            cs.AddPhoto(request.PhotoPath);
            await _repo.UpdateAsync(cs, ct);

            return cs.Photos.Last().Id;
        }
    }
}
