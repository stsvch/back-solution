using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.UpdateNews
{
    public class UpdateNewsCommandHandler
        : IRequestHandler<UpdateNewsCommand, Unit>
    {
        private readonly IRepository<Back.Domain.Entities.News> _repo;

        public UpdateNewsCommandHandler(IRepository<Back.Domain.Entities.News> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            UpdateNewsCommand request,
            CancellationToken cancellationToken)
        {
            var news = await _repo.GetByIdAsync(request.Id, cancellationToken)
                       ?? throw new KeyNotFoundException("News not found");

            news.ChangeTitle(request.Title);
            news.ChangeDescription(request.Description);

            // Перестроить коллекцию фото
            foreach (var pic in news.Photos.ToList())
                news.RemovePhoto(pic);
            foreach (var p in request.PhotoPaths)
                news.AddPhoto(p);

            await _repo.UpdateAsync(news, cancellationToken);
            return Unit.Value;
        }
    }
}
