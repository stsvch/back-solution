﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.RemovePhotoFromDevelopment
{
    public record RemovePhotoFromDevelopmentCommand(
        Guid DevelopmentId,
        Guid PhotoId
    ) : IRequest<Unit>;
}
