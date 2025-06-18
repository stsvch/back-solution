using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Queries.GetById
{
    public class GetCaseStudyByIdQueryValidator
        : AbstractValidator<GetCaseStudyByIdQuery>
    {
        public GetCaseStudyByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
