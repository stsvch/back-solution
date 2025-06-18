using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.Developments.Queries.GetById
{
    public class GetDevelopmentByIdQueryValidator
        : AbstractValidator<GetDevelopmentByIdQuery>
    {
        public GetDevelopmentByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
