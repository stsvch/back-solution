using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.CreateDevelopment
{
    public record CreateDevelopmentCommand(
        string Title,
        string Description,
        List<string> PhotoPaths
    ) : IRequest<Guid>;
}
