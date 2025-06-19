using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.AddPhotoToDevelopment
{
    public class AddPhotoToDevelopmentCommandValidator
        : AbstractValidator<AddPhotoToDevelopmentCommand>
    {
        public AddPhotoToDevelopmentCommandValidator()
        {
            RuleFor(x => x.DevelopmentId).NotEmpty();
            RuleFor(x => x.PhotoPath)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
