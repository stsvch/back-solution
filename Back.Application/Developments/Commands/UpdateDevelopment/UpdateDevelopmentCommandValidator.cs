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

        }
    }
}
