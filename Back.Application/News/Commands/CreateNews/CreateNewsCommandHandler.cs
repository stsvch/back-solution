using Back.Domain.Repositories;
using MediatR;

namespace Back.Application.News.Commands.CreateNews
{
    public class CreateNewsCommandHandler
        : IRequestHandler<CreateNewsCommand, Guid>
    {
        private readonly IRepository<Back.Domain.Entities.News> _repo;

        public CreateNewsCommandHandler(IRepository<Back.Domain.Entities.News> repo)
            => _repo = repo;

        public async Task<Guid> Handle(
            CreateNewsCommand request,
            CancellationToken cancellationToken)
        {
            var news = new Back.Domain.Entities.News(request.Title, request.Description);
            foreach (var p in request.PhotoPaths)
                news.AddPhoto(p);
            await _repo.AddAsync(news, cancellationToken);
            return news.Id;
        }
    }
}
