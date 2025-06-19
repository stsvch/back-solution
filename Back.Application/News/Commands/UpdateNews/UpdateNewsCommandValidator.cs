using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.UpdateNews
{
    public class UpdateNewsCommandValidator
        : AbstractValidator<UpdateNewsCommand>
    {
        public UpdateNewsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description)
                .NotEmpty().MaximumLength(2000);
            RuleForEach(x => x.PhotoPaths)
                .NotEmpty();
        }
    }
}
