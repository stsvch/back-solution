using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Queries.GetById
{
    public class GetNewsByIdQueryValidator
        : AbstractValidator<GetNewsByIdQuery>
    {
        public GetNewsByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
