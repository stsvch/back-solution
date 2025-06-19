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
        public AddPhotoToNewsCommandHandler(IContentRepository<Back.Domain.Entities.News> repo)
            => _repo = repo;

        public Task<Guid> Handle(AddPhotoToNewsCommand req, CancellationToken ct)
            => _repo.AddPhotoAsync(req.NewsId, req.PhotoPath, ct);
    }
}
