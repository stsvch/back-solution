using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.AddPhotoToDevelopment
{
    public class AddPhotoToDevelopmentCommandHandler
            : IRequestHandler<AddPhotoToDevelopmentCommand, Guid>
    {
        private readonly IContentRepository<Development> _repo;
        public AddPhotoToDevelopmentCommandHandler(
            IContentRepository<Development> repo) => _repo = repo;

        public async Task<Guid> Handle(
            AddPhotoToDevelopmentCommand request,
            CancellationToken ct)
        {
            // 1) Загружаем агрегат вместе с фото
            var dev = await _repo.GetByIdWithPhotosAsync(request.DevelopmentId, ct)
                      ?? throw new KeyNotFoundException("Development not found");

            // 2) Вызываем доменный метод
            dev.AddPhoto(request.PhotoPath);

            // 3) Сохраняем агрегат
            await _repo.UpdateAsync(dev, ct);

            // 4) Последний добавленный фото — это последний в коллекции
            return dev.Photos.Last().Id;
        }
    }
}
