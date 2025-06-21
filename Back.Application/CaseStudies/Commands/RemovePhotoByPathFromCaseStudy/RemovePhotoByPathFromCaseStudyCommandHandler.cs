using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.RemovePhotoByPathFromCaseStudy
{
    public class RemovePhotoByPathFromCaseStudyCommandHandler
            : IRequestHandler<RemovePhotoByPathFromCaseStudyCommand, Unit>
    {
        private readonly IContentRepository<CaseStudy> _repo;

        public RemovePhotoByPathFromCaseStudyCommandHandler(
            IContentRepository<CaseStudy> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            RemovePhotoByPathFromCaseStudyCommand request,
            CancellationToken ct)
        {
            // Загружаем кейс вместе с фото
            var cs = await _repo.GetByIdWithPhotosAsync(request.CaseStudyId, ct)
                     ?? throw new KeyNotFoundException("CaseStudy not found");

            // Находим фото по пути
            var photo = cs.Photos
                          .FirstOrDefault(p => p.Path == request.Path)
                        ?? throw new KeyNotFoundException("Photo not found by path");

            // Удаляем его из агрегата
            cs.RemovePhoto(photo);

            // Сохраняем изменения
            await _repo.UpdateAsync(cs, ct);
            return Unit.Value;
        }
    }
}
