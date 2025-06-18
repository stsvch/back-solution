using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.DeleteCaseStudy
{
    public class DeleteCaseStudyCommandValidator
        : AbstractValidator<DeleteCaseStudyCommand>
    {
        public DeleteCaseStudyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
