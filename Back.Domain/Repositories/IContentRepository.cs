using Back.Application.Common;
using Back.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Repositories
{
    public interface IContentRepository<T> : IRepository<T>
        where T : ContentBase
    {
        Task<T?> GetByIdWithPhotosAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<PagedList<T>> GetPagedWithPhotosAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<Guid> AddPhotoAsync(Guid id, string photoPath, CancellationToken cancellationToken = default);
    }
}
