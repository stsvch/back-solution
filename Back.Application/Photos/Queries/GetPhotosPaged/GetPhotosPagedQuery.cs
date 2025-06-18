using Back.Application.Common;
using Back.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Queries.GetPhotosPaged
{
    public record GetPhotosPagedQuery(int PageNumber, int PageSize)
        : IRequest<PagedList<PhotoDto>>;

}
