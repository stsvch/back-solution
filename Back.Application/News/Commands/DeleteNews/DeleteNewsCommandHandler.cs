using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.DeleteNews
{
    public class DeleteNewsCommandHandler
        : IRequestHandler<DeleteNewsCommand, Unit>
    {
        private readonly IRepository<Back.Domain.Entities.News> _repo;

        public DeleteNewsCommandHandler(IRepository<Back.Domain.Entities.News> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            DeleteNewsCommand request,
            CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
