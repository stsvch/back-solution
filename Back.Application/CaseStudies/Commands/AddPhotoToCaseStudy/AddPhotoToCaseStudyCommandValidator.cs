using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.AddPhotoToCaseStudy
{
    public class AddPhotoToCaseStudyCommandValidator
        : AbstractValidator<AddPhotoToCaseStudyCommand>
    {
        public AddPhotoToCaseStudyCommandValidator()
        {
            RuleFor(x => x.CaseStudyId).NotEmpty();
            RuleFor(x => x.PhotoPath)
                .NotEmpty().MaximumLength(500);
        }
    }
}
