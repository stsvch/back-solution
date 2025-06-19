using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.AddPhotoToDevelopment
{
    public class AddPhotoToDevelopmentCommandHandler
        : IRequestHandler<AddPhotoToDevelopmentCommand, Guid>
    {
        private readonly IContentRepository<Development> _repo;
        public AddPhotoToDevelopmentCommandHandler(IContentRepository<Development> repo)
            => _repo = repo;

        public Task<Guid> Handle(AddPhotoToDevelopmentCommand req, CancellationToken ct)
            => _repo.AddPhotoAsync(req.DevelopmentId, req.PhotoPath, ct);
    }
}
