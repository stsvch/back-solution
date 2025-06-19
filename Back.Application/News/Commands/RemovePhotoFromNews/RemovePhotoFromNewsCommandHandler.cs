using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.RemovePhotoFromNews
{
    public class RemovePhotoFromNewsCommandHandler
            : IRequestHandler<RemovePhotoFromNewsCommand, Unit>
    {
        private readonly IContentRepository<Back.Domain.Entities.News> _repo;

        public RemovePhotoFromNewsCommandHandler(
            IContentRepository<Back.Domain.Entities.News> repo) => _repo = repo;

        public async Task<Unit> Handle(
            RemovePhotoFromNewsCommand request,
            CancellationToken ct)
        {
            var news = await _repo.GetByIdWithPhotosAsync(
                           request.NewsId, ct)
                       ?? throw new KeyNotFoundException("News not found");

            var photo = news.Photos.FirstOrDefault(p => p.Id == request.PhotoId)
                        ?? throw new KeyNotFoundException("Photo not found");
            news.RemovePhoto(photo);
            await _repo.UpdateAsync(news, ct);
            return Unit.Value;
        }
    }
}
