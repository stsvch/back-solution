using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Commands.CreatePhoto
{
    public record CreatePhotoCommand(string Path) : IRequest<Guid>;
}
