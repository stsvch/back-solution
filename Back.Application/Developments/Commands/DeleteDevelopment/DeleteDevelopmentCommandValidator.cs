using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Commands.DeleteDevelopment
{
    public class DeleteDevelopmentCommandValidator
        : AbstractValidator<DeleteDevelopmentCommand>
    {
        public DeleteDevelopmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
