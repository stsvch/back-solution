using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.AddPhotoToDevelopment
{
    public record AddPhotoToDevelopmentCommand(
        Guid DevelopmentId,
        string PhotoPath
    ) : IRequest<Guid>;
}
