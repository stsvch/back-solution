// Back.Application/Photos/Commands/UpdatePhoto/UpdatePhotoCommandHandler.cs
using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;

namespace Back.Application.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommandHandler
        : IRequestHandler<UpdatePhotoCommand, Unit>
    {
        private readonly IRepository<Photo> _repo;
        public UpdatePhotoCommandHandler(IRepository<Photo> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            UpdatePhotoCommand request,
            CancellationToken cancellationToken)
        {
            // получаем существующую сущность
            var photo = await _repo.GetByIdAsync(request.Id, cancellationToken)
                        ?? throw new KeyNotFoundException("Photo not found");

            // изменяем поле Path
            photo.ChangePath(request.Path);

            // сохраняем изменения
            await _repo.UpdateAsync(photo, cancellationToken);

            return Unit.Value;
        }
    }
}
