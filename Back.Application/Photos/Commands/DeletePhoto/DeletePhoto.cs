using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Commands.DeletePhoto
{
    public record DeletePhotoCommand(Guid Id) : IRequest<Unit>;
}
