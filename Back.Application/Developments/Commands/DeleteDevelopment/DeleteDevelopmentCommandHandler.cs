using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.DeleteDevelopment
{
    public class DeleteDevelopmentCommandHandler
        : IRequestHandler<DeleteDevelopmentCommand, Unit>
    {
        private readonly IRepository<Development> _repo;

        public DeleteDevelopmentCommandHandler(IRepository<Development> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            DeleteDevelopmentCommand request,
            CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }

    }
}
