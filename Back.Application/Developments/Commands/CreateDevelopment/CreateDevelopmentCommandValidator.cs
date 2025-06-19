using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.CreateDevelopment
{
    public class CreateDevelopmentCommandValidator
        : AbstractValidator<CreateDevelopmentCommand>
    {
        public CreateDevelopmentCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description)
                .NotEmpty().MaximumLength(2000);
        }
    }
}
