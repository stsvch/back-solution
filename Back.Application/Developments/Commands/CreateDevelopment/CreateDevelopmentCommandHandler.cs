using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.CreateDevelopment
{
    public class CreateDevelopmentCommandHandler
        : IRequestHandler<CreateDevelopmentCommand, Guid>
    {
        private readonly IRepository<Development> _repo;

        public CreateDevelopmentCommandHandler(IRepository<Development> repo)
            => _repo = repo;

        public async Task<Guid> Handle(
            CreateDevelopmentCommand request,
            CancellationToken cancellationToken)
        {
            var dev = new Development(request.Title, request.Description);
            foreach (var path in request.PhotoPaths)
                dev.AddPhoto(path);

            await _repo.AddAsync(dev, cancellationToken);
            return dev.Id;
        }
    }
}
