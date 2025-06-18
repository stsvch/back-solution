using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandHandler
        : IRequestHandler<DeletePhotoCommand, Unit>
    {
        private readonly IRepository<Photo> _repo;
        public DeletePhotoCommandHandler(IRepository<Photo> repo) => _repo = repo;

        public async Task<Unit> Handle(
            DeletePhotoCommand request,
            CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
