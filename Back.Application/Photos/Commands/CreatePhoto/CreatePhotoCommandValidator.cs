using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommandValidator
        : AbstractValidator<CreatePhotoCommand>
    {
        public CreatePhotoCommandValidator()
        {
            RuleFor(x => x.Path)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
