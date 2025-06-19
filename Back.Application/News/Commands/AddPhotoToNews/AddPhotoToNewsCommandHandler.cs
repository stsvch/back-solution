using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.AddPhotoToNews
{
    public class AddPhotoToNewsCommandHandler
            : IRequestHandler<AddPhotoToNewsCommand, Guid>
    {
        private readonly IContentRepository<Back.Domain.Entities.News> _repo;

        public AddPhotoToNewsCommandHandler(
            IContentRepository<Back.Domain.Entities.News> repo) => _repo = repo;

        public async Task<Guid> Handle(
            AddPhotoToNewsCommand request,
            CancellationToken ct)
        {
            var news = await _repo.GetByIdWithPhotosAsync(
                           request.NewsId, ct)
                       ?? throw new KeyNotFoundException("News not found");

            news.AddPhoto(request.PhotoPath);
            await _repo.UpdateAsync(news, ct);
            return news.Photos.Last().Id;
        }
    }
}
