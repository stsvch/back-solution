using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Queries.GetById
{
    public class GetPhotoByIdQueryValidator
        : AbstractValidator<GetPhotoByIdQuery>
    {
        public GetPhotoByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
