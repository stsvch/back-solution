using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Queries.GetPaged
{
    public class GetNewsPagedQueryValidator
        : AbstractValidator<GetNewsPagedQuery>
    {
        public GetNewsPagedQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
