using Back.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Queries.GetById
{
    public record GetPhotoByIdQuery(Guid Id) : IRequest<PhotoDto>;
}
