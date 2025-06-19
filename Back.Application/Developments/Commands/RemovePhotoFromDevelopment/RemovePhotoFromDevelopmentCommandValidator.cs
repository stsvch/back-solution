using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.RemovePhotoFromDevelopment
{
    public class RemovePhotoFromDevelopmentCommandValidator
        : AbstractValidator<RemovePhotoFromDevelopmentCommand>
    {
        public RemovePhotoFromDevelopmentCommandValidator()
        {
            RuleFor(x => x.DevelopmentId).NotEmpty();
            RuleFor(x => x.PhotoId).NotEmpty();
        }
    }
}
