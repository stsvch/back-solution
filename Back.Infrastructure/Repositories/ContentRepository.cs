using Back.Application.Common;
using Back.Domain.Entities;
using Back.Domain.Repositories;
using Back.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Infrastructure.Repositories
{
    public class ContentRepository<T>
            : EfRepository<T>, IContentRepository<T>
            where T : ContentBase
    {
        private readonly AppDbContext _context;

        public ContentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdWithPhotosAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>()
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<PagedList<T>> GetPagedWithPhotosAsync(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>().Include(x => x.Photos);

            var total = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(items, total);
        }

        public async Task<Guid> AddPhotoAsync(
                Guid parentId,
                string photoPath,
                CancellationToken cancellationToken = default)
        {
            // 1) проверяем, что родитель существует
            var exists = await _context.Set<T>()
                .AsNoTracking()
                .AnyAsync(x => x.Id == parentId, cancellationToken);
            if (!exists)
                throw new KeyNotFoundException($"{typeof(T).Name} {parentId} not found");

            // 2) создаём новую Photo
            var photo = new Photo(photoPath);

            // 3) добавляем её в контекст
            _context.Set<Photo>().Add(photo);

            // 4) динамически выставляем FK-колонку
            //    e.g. T = Development  → fkName = "DevelopmentId"
            var fkName = typeof(T).Name + "Id";
            _context.Entry(photo)
                .Property(fkName)
                .CurrentValue = parentId;

            // 5) сохраняем — INSERT Photos
            await _context.SaveChangesAsync(cancellationToken);

            return photo.Id;
        }
    }
}
