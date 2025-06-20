﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.RemovePhotoFromNews
{
    public record RemovePhotoFromNewsCommand(
        Guid NewsId,
        Guid PhotoId
    ) : IRequest<Unit>;
}
