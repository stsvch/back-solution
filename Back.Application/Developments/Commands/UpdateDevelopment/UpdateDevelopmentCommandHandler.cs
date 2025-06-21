using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.UpdateDevelopment
{
    public class UpdateDevelopmentCommandHandler
            : IRequestHandler<UpdateDevelopmentCommand, Unit>
    {
        private readonly IRepository<Development> _repo;

        public UpdateDevelopmentCommandHandler(IRepository<Development> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            UpdateDevelopmentCommand request,
            CancellationToken cancellationToken)
        {
            var dev = await _repo.GetByIdAsync(request.Id, cancellationToken)
                      ?? throw new KeyNotFoundException("Development not found");

            dev.ChangeTitle(request.Title);
            dev.ChangeDescription(request.Description);

            await _repo.UpdateAsync(dev, cancellationToken);
            return Unit.Value;
        }
    }
}
