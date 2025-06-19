using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.RemovePhotoFromCaseStudy
{
    public class RemovePhotoFromCaseStudyCommandValidator
        : AbstractValidator<RemovePhotoFromCaseStudyCommand>
    {
        public RemovePhotoFromCaseStudyCommandValidator()
        {
            RuleFor(x => x.CaseStudyId).NotEmpty();
            RuleFor(x => x.PhotoId).NotEmpty();
        }
    }
}
