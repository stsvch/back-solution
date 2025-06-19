using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.AddPhotoToNews
{
    public record AddPhotoToNewsCommand(
        Guid NewsId,
        string PhotoPath
    ) : IRequest<Guid>;
}
