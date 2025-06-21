using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.RemovePhotoByPathFromNews
{
    public class RemovePhotoByPathFromNewsCommandHandler
            : IRequestHandler<RemovePhotoByPathFromNewsCommand, Unit>
    {
        private readonly IContentRepository<Back.Domain.Entities.News> _repo;

        public RemovePhotoByPathFromNewsCommandHandler(
            IContentRepository<Back.Domain.Entities.News> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            RemovePhotoByPathFromNewsCommand request,
            CancellationToken ct)
        {
            // 1) Загружаем новость вместе с фотографиями
            var news = await _repo.GetByIdWithPhotosAsync(request.NewsId, ct)
                       ?? throw new KeyNotFoundException("News not found");

            // 2) Находим фотографию по относительному пути
            var photo = news.Photos
                            .FirstOrDefault(p => p.Path == request.Path)
                        ?? throw new KeyNotFoundException("Photo not found by path");

            // 3) Удаляем её из агрегата (метод RemovePhoto реализует и удаление связи, и сам файл, если нужно)
            news.RemovePhoto(photo);

            // 4) Сохраняем изменения
            await _repo.UpdateAsync(news, ct);
            return Unit.Value;
        }
    }
}
