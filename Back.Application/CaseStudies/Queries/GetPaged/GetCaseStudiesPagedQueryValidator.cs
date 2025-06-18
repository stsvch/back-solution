using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Queries.GetPaged
{
    public class GetCaseStudiesPagedQueryValidator
        : AbstractValidator<GetCaseStudiesPagedQuery>
    {
        public GetCaseStudiesPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
