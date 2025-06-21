using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.UpdateCaseStudy
{
    public class UpdateCaseStudyCommandValidator
        : AbstractValidator<UpdateCaseStudyCommand>
    {
        public UpdateCaseStudyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

        }
    }
}
