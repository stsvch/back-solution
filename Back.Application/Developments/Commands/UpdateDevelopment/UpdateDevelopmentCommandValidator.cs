using Back.Application.Developments.Commands.CreateDevelopment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.UpdateDevelopment
{
    public class UpdateDevelopmentCommandValidator
        : AbstractValidator<UpdateDevelopmentCommand>
    {
        public UpdateDevelopmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            // Вставляем сюда же все правила из CreateDevelopmentCommandValidator:
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleForEach(x => x.PhotoPaths)
                .NotEmpty();
        }
    }
}
