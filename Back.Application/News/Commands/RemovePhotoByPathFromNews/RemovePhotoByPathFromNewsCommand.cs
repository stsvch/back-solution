using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.RemovePhotoByPathFromNews
{
    public record RemovePhotoByPathFromNewsCommand(
        Guid NewsId,
        string Path
    ) : IRequest<Unit>;
}
