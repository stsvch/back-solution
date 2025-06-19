using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.CreateNews
{
    public class CreateNewsCommandValidator
        : AbstractValidator<CreateNewsCommand>
    {
        public CreateNewsCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description)
                .NotEmpty().MaximumLength(2000);
            RuleForEach(x => x.PhotoPaths)
                .NotEmpty();
        }
    }
}
